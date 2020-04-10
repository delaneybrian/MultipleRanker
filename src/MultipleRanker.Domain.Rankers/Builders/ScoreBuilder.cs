using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace MultipleRanker.Domain.Rankers.Builders
{
    public class ScoreBuilder : IBuilder
    {
        public Matrix<double> Generate(RankingBoardModel rankingRankingBoardModel)
        {
            var numberOfParticipants = rankingRankingBoardModel.ParticipantRankingModels.Count;

            var scoreMatrix = Matrix<double>.Build.Dense(numberOfParticipants, numberOfParticipants);

            foreach (var participantRankingModel in rankingRankingBoardModel.ParticipantRankingModels.OrderBy(x => x.Id))
            {
                foreach (var opponentRankingModel in participantRankingModel.TotalScoreByOpponentId)
                {

                }
            }

            return scoreMatrix;
        }
    }
}
