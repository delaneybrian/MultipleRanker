using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Repositories
{
    public class InMemoryRatingListSnapshotRepository : IListSnapshotRepository
    {
        private static Dictionary<Guid, RatingListSnapshot> _ratingListssById =
            new Dictionary<Guid, RatingListSnapshot>();

        public async Task<RatingListSnapshot> Get(Guid ratingListId)
        {
            if (_ratingListssById.TryGetValue(ratingListId, out var ratingList))
                return ratingList;

            throw new ArgumentException($"rating list with id {ratingListId} not in state");
        }

        public async Task Set(RatingListSnapshot ratingListSnapshot)
        {
            _ratingListssById[ratingListSnapshot.Id] = ratingListSnapshot;
        }
    }
}
