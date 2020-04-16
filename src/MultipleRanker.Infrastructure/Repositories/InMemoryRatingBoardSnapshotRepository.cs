using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Repositories
{
    public class InMemoryRatingBoardSnapshotRepository : IRatingBoardSnapshotRepository
    {
        private static Dictionary<Guid, RatingBoardSnapshot> _ratingBoardsById =
            new Dictionary<Guid, RatingBoardSnapshot>();

        public async Task<RatingBoardSnapshot> Get(Guid ratingBoardId)
        {
            if (_ratingBoardsById.TryGetValue(ratingBoardId, out var ratingBoard))
                return ratingBoard;

            throw new ArgumentException($"rating board with id {ratingBoardId} not in state");
        }

        public async Task Set(RatingBoardSnapshot ratingBoardSnapshot)
        {
            _ratingBoardsById[ratingBoardSnapshot.Id] = ratingBoardSnapshot;
        }
    }
}
