using System;
using System.Threading.Tasks;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Interfaces
{
    public interface IRatingBoardSnapshotRepository
    {
        Task<RatingBoardSnapshot> Get(Guid ratingBoardId);

        Task Set(RatingBoardSnapshot ratingBoardSnapshot);
    }
}
