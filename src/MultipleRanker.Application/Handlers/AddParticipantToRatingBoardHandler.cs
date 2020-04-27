using System;
using System.Threading.Tasks;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class AddParticipantToRatingBoardHandler : IHandler<AddParticipantToRatingBoard>
    {
        private readonly IRatingBoardSnapshotRepository _ratingBoardSnapshotRepository;

        public AddParticipantToRatingBoardHandler(IRatingBoardSnapshotRepository ratingBoardSnapshotRepository)
        {
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
        }

        public async Task HandleAsync(AddParticipantToRatingBoard evt)
        {
            try
            {
                var ratingBoardSnapshot = await _ratingBoardSnapshotRepository.Get(evt.RankingBoardId);

                var ratingBoardModel = RatingBoardModel.For(ratingBoardSnapshot);

                ratingBoardModel.Apply(evt);

                var updatedSnapshot = ratingBoardModel.ToSnapshot();

                await _ratingBoardSnapshotRepository.Set(updatedSnapshot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
