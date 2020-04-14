using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Raters
{
    public interface IRater
    {
        bool IsFor(RaterType rankerType);

        RatingResults Rate(RankingBoardModel rankingBoardModel);
    }
}
