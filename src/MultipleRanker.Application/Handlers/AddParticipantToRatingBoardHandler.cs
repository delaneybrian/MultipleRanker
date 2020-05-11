using System;
using System.Threading.Tasks;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class AddParticipantToRatingBoardHandler : IHandler<AddParticipantToRatingBoard>
    {
        private readonly IListSnapshotRepository _ratingListSnapshotRepository;

        public AddParticipantToRatingBoardHandler(IListSnapshotRepository ratingListSnapshotRepository)
        {
            _ratingListSnapshotRepository = ratingListSnapshotRepository;
        }

        public async Task HandleAsync(AddParticipantToRatingBoard evt)
        {
            try
            {
                var ratingListSnapshot = await _ratingListSnapshotRepository.Get(evt.RankingBoardId);

                var ratingListModel = RatingListModel.For(ratingListSnapshot);

                ratingListModel.Apply(evt);

                var updatedSnapshot = ratingListModel.ToSnapshot();

                await _ratingListSnapshotRepository.Set(updatedSnapshot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
