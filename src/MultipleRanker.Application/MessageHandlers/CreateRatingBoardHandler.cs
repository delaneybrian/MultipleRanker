using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.MessageHandlers
{
    public class CreateRatingBoardHandler : AsyncRequestHandler<CreateRatingBoard>
    {
        private readonly IRatingBoardSnapshotRepository _ratingBoardSnapshotRepository;

        public CreateRatingBoardHandler(IRatingBoardSnapshotRepository ratingBoardSnapshotRepository)
        {
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
        }

        protected override async Task Handle(CreateRatingBoard command, CancellationToken cancellationToken)
        {
            var model = new RatingBoardModel();

            model.Apply(command);

            var snapshot = model.ToSnapshot();

            await _ratingBoardSnapshotRepository.Set(snapshot);
        }
    }
}
