using MathNet.Numerics.LinearAlgebra;

namespace MultipleRanker.Domain.Raters
{
    public interface IGenerator
    {
        bool IsFor(GeneratorType generatorType);

        Matrix<double> Generate(RatingListModel ratingRankingBoardModel);
    }
}
