using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace MultipleRanker.Domain.Raters.Generators
{
    public class PointsDifferentGenerator : IGenerator
    {
        public Matrix<double> Generate(RatingBoardModel ratingRankingBoardModel)
        {
            var numberOfParticipants = ratingRankingBoardModel.ParticipantRatingModels.Count;

            var pointsDifferenceMatrix = Matrix<double>.Build.Dense(numberOfParticipants, numberOfParticipants);

            int i = 0;
            foreach (var participantRankingModel in ratingRankingBoardModel
                .ParticipantRatingModels
                .OrderBy(x => x.Index))
            {
                int j = 0;
                foreach (var opponentParticipantRankingModel in ratingRankingBoardModel
                    .ParticipantRatingModels
                    .OrderBy(x => x.Index))
                {
                    if (participantRankingModel.Id == opponentParticipantRankingModel.Id)
                    {
                        pointsDifferenceMatrix[j, i] = 0;
                    }
                    else
                    {
                        var score = participantRankingModel.TotalScoreByOpponentId[opponentParticipantRankingModel.Id];
                        var opponentScore = opponentParticipantRankingModel.TotalScoreByOpponentId[participantRankingModel.Id];

                        var pointsDifference = score - opponentScore;

                        if (pointsDifference > 0)
                            pointsDifferenceMatrix[j, i] = pointsDifference;
                        else
                            pointsDifferenceMatrix[j, i] = 0;
                    }

                    j++;
                }

                i++;
            }

            return pointsDifferenceMatrix;
        }

        public bool IsFor(GeneratorType generatorType)
        {
            return generatorType == GeneratorType.PointsDifference;
        }
    }
}
