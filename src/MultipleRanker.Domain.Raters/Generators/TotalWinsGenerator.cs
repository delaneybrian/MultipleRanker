using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace MultipleRanker.Domain.Raters.Generators
{
    public class TotalWinsGenerator : IGenerator
    {
        public Matrix<double> Generate(RatingListModel ratingRankingBoardModel)
        {
            var numberOfParticipants = ratingRankingBoardModel.ParticipantRatingModels.Count;

            var winsMatrix = Matrix<double>.Build.Dense(numberOfParticipants, numberOfParticipants);

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
                        winsMatrix[j, i] = 0;
                    }
                    else
                    {
                        var score = participantRankingModel.TotalWinsByOpponentId[opponentParticipantRankingModel.Id];
                        winsMatrix[j, i] = score;
                    }

                    j++;
                }

                i++;
            }

            return winsMatrix;
        }

        public bool IsFor(GeneratorType generatorType)
        {
            return generatorType == GeneratorType.TotalWins;
        }
    }
}
