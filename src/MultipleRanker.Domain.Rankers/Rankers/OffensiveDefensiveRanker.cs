using System;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Rankers
{
    public class OffensiveDefensiveRanker : IRanker
    {
        public bool IsFor(RankerType rankerType)
        {
            return rankerType == RankerType.OffensiveDefensive;
        }

        public RatingResults Rank(RankingBoardModel rankingBoardModel)
        {
            throw new NotImplementedException();
        }
    }
}
