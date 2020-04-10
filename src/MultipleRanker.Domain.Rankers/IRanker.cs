namespace MultipleRanker.Domain.Rankers
{
    public interface IRanker
    {
        bool IsFor(RankerType rankerType);

        void Rank(RankingBoardModel rankingBoardModel);
    }
}
