using System.Threading.Tasks;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;
using MultipleRanker.RankerApi.Contracts.Events;

namespace MultipleRanker.Application.CommandHandlers
{
    public class RatingListCreatedHandler : IHandler<RatingListCreated>
    {
        private readonly IListSnapshotRepository _ratingListSnapshotRepository;

        public RatingListCreatedHandler(IListSnapshotRepository ratingListSnapshotRepository)
        {
            _ratingListSnapshotRepository = ratingListSnapshotRepository;
        }

        public async Task HandleAsync(RatingListCreated evt)
        {
            var model = new RatingListModel();

            model.Apply(evt);

            var snapshot = model.ToSnapshot();

            await _ratingListSnapshotRepository.Set(snapshot);
        }
    }
}
