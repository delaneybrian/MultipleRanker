using MathNet.Numerics.LinearAlgebra;

namespace MultipleRanker.Domain.Rankers
{
    public interface IBuilder
    {
        Matrix<double> Generate(RankingBoardModel rankingRankingBoardModel);
    }
}
