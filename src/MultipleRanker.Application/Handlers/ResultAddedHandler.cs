using System.Threading.Tasks;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;
using MultipleRanker.RankerApi.Contracts.Events;

namespace MultipleRanker.Application.CommandHandlers
{
    public class ResultAddedHandler : IHandler<ResultAdded>
    {
        private readonly IListSnapshotRepository _ratingListSnapshotRepository;

        public ResultAddedHandler(IListSnapshotRepository ratingListSnapshotRepository)
        {
            _ratingListSnapshotRepository = ratingListSnapshotRepository;
        }

        public async Task HandleAsync(ResultAdded evt)
        {
            var ratingListSnapshot = await _ratingListSnapshotRepository.Get(evt.RatingListId);

            var ratingListModel = RatingListModel.For(ratingListSnapshot);

            ratingListModel.Apply(evt);

            var updatedSnapshot = ratingListModel.ToSnapshot();

            await _ratingListSnapshotRepository.Set(updatedSnapshot);
        }
    }
}
