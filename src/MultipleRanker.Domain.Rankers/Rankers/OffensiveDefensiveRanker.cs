using System;

namespace MultipleRanker.Domain.Rankers
{
    public class OffensiveDefensiveRanker : IRanker
    {
        public bool IsFor(RankerType rankerType)
        {
            return rankerType == RankerType.OffensiveDefensive;
        }

        public void Rank(RankingBoardModel rankingBoardModel)
        {
            throw new NotImplementedException();
        }
    }
}
