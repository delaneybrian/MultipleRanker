using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace MultipleRanker.Domain.Raters.Generators
{
    public class TotalScoreGenerator : IGenerator
    {
        public Matrix<double> Generate(RatingBoardModel ratingRankingBoardModel)
        {
            var numberOfParticipants = ratingRankingBoardModel.ParticipantRatingModels.Count;

            var scoreMatrix = Matrix<double>.Build.Dense(numberOfParticipants, numberOfParticipants);

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
                        scoreMatrix[j, i] = 0;
                    }
                    else
                    {
                        var score = participantRankingModel.TotalScoreByOpponentId[opponentParticipantRankingModel.Id];
                        scoreMatrix[j, i] = score;
                    }

                    j++;
                }

                i++;
            }

            return scoreMatrix;
        }

        public bool IsFor(GeneratorType generatorType)
        {
            return generatorType == GeneratorType.TotalScore;
        }
    }
}