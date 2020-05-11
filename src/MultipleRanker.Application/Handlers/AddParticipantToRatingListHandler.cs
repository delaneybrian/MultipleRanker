using System;
using System.Threading.Tasks;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;
using MultipleRanker.RankerApi.Contracts.Events;

namespace MultipleRanker.Application.CommandHandlers
{
    public class AddParticipantToRatingListHandler : IHandler<ParticipantAddedToRatingList>
    {
        private readonly IListSnapshotRepository _ratingListSnapshotRepository;

        public AddParticipantToRatingListHandler(IListSnapshotRepository ratingListSnapshotRepository)
        {
            _ratingListSnapshotRepository = ratingListSnapshotRepository;
        }

        public async Task HandleAsync(ParticipantAddedToRatingList evt)
        {
            try
            {
                var ratingListSnapshot = await _ratingListSnapshotRepository.Get(evt.RatingListId);

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
