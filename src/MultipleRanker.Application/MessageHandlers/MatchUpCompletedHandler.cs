using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.MessageHandlers
{
    public class MatchUpCompletedHandler : AsyncRequestHandler<MatchUpCompleted>
    {
        private readonly IRatingBoardSnapshotRepository _ratingBoardSnapshotRepository;

        public MatchUpCompletedHandler(IRatingBoardSnapshotRepository ratingBoardSnapshotRepository)
        {
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
        }

        protected override async Task Handle(MatchUpCompleted cmd, CancellationToken cancellationToken)
        {
            var ratingBoardSnapshot = await _ratingBoardSnapshotRepository.Get(cmd.RatingBoardId);

            var ratingBoardModel = RatingBoardModel.For(ratingBoardSnapshot);

            ratingBoardModel.Apply(cmd);

            var updatedSnapshot = ratingBoardModel.ToSnapshot();

            await _ratingBoardSnapshotRepository.Set(updatedSnapshot);
        }
    }
}
