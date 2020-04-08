using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Repositories
{
    public class InMemoryRankingBoardSnapshotRepository : IRankingBoardSnapshotRepository
    {
        private static Dictionary<Guid, RankingBoardSnapshot> _rankingBoardsById =
            new Dictionary<Guid, RankingBoardSnapshot>();

        public async Task<RankingBoardSnapshot> Get(Guid rankingBoardId)
        {
            if (_rankingBoardsById.TryGetValue(rankingBoardId, out var rankingBoard))
                return rankingBoard;

            throw new ArgumentException($"ranking board with id {rankingBoardId} not in state");
        }

        public async Task Set(RankingBoardSnapshot rankingBoardSnapshot)
        {
            _rankingBoardsById[rankingBoardSnapshot.Id] = rankingBoardSnapshot;
        }
    }
}
