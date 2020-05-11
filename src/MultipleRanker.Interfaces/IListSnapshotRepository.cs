using System;
using System.Threading.Tasks;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Interfaces
{
    public interface IListSnapshotRepository
    {
        Task<RatingListSnapshot> Get(Guid ratingListId);

        Task Set(RatingListSnapshot ratingListSnapshot);
    }
}
