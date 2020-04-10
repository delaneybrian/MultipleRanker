using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Definitions.Commands;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class MatchUpCompletedCommandHandler : AsyncRequestHandler<MatchUpCompletedCommand>
    {
        private readonly IRankingBoardSnapshotRepository _rankingBoardSnapshotRepository;

        public MatchUpCompletedCommandHandler(IRankingBoardSnapshotRepository rankingBoardSnapshotRepository)
        {
            _rankingBoardSnapshotRepository = rankingBoardSnapshotRepository;
        }

        protected override async Task Handle(MatchUpCompletedCommand cmd, CancellationToken cancellationToken)
        {
            var rankingBoardSnapshot = await _rankingBoardSnapshotRepository.Get(cmd.RankingBoardId);

            var rankingBoardModel = RankingBoardModel.For(rankingBoardSnapshot);

            rankingBoardModel.Apply(cmd);

            var updatedSnapshot = rankingBoardModel.ToSnapshot();

            await _rankingBoardSnapshotRepository.Set(updatedSnapshot);
        }
    }
}
