using System.Threading.Tasks;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class CreateRatingBoardHandler : IHandler<CreateRatingBoard>
    {
        private readonly IRatingBoardSnapshotRepository _ratingBoardSnapshotRepository;

        public CreateRatingBoardHandler(IRatingBoardSnapshotRepository ratingBoardSnapshotRepository)
        {
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
        }

        public async Task HandleAsync(CreateRatingBoard evt)
        {
            var model = new RatingBoardModel();

            model.Apply(evt);

            var snapshot = model.ToSnapshot();

            await _ratingBoardSnapshotRepository.Set(snapshot);
        }
    }
}
