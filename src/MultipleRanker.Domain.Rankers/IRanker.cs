using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Rankers
{
    public interface IRanker
    {
        bool IsFor(RankerType rankerType);

        RatingResults Rank(RankingBoardModel rankingBoardModel);
    }
}
