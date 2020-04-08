using System;
using System.Threading.Tasks;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Interfaces
{
    public interface IRankingBoardSnapshotRepository
    {
        Task<RankingBoardSnapshot> Get(Guid rankingBoardId);

        Task Set(RankingBoardSnapshot rankingBoardSnapshot);
    }
}
