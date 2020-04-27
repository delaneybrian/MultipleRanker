using System.Threading.Tasks;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class MatchUpCompletedHandler : IHandler<MatchUpCompleted>
    {
        private readonly IRatingBoardSnapshotRepository _ratingBoardSnapshotRepository;

        public MatchUpCompletedHandler(IRatingBoardSnapshotRepository ratingBoardSnapshotRepository)
        {
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
        }

        public async Task HandleAsync(MatchUpCompleted evt)
        {
            var ratingBoardSnapshot = await _ratingBoardSnapshotRepository.Get(evt.RatingBoardId);

            var ratingBoardModel = RatingBoardModel.For(ratingBoardSnapshot);

            ratingBoardModel.Apply(evt);

            var updatedSnapshot = ratingBoardModel.ToSnapshot();

            await _ratingBoardSnapshotRepository.Set(updatedSnapshot);
        }
    }
}
