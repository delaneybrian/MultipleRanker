using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Domain.Raters;
using MultipleRanker.Interfaces;
using MultipleRanker.RankerApi.Contracts.Events;

namespace MultipleRanker.Application.CommandHandlers
{
    public class GenerateRatingsHandler : IHandler<GenerateRatings>
    {
        private readonly IEnumerable<IRater> _raters;
        private readonly IListSnapshotRepository _ratingListSnapshotRepository;
        private readonly IMessagePublisher _messagePublisher;

        public GenerateRatingsHandler(IEnumerable<IRater> raters,
            IListSnapshotRepository ratingListSnapshotRepository,
            IMessagePublisher messagePublisher)
        {
            _raters = raters;
            _ratingListSnapshotRepository = ratingListSnapshotRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task HandleAsync(GenerateRatings evt)
        {
            var ratingListSnapshot = await _ratingListSnapshotRepository.Get(evt.RatingListId);

            var ratingListModel = RatingListModel.For(ratingListSnapshot);

            ratingListModel.Apply(evt);

            var rater = _raters.Single(r => r.IsFor(evt.RatingType.ToRankerType()));

            var ratingsResults = rater.Rate(ratingListModel);

            var ratingsGeneratedCommand = new RatingsGenerated
            {
                RatingId = Guid.NewGuid(),
                CalculatedAtUtc = DateTime.UtcNow,
                RatingListId = evt.RatingListId,
                RatingType = evt.RatingType,
                ParticipantRatings = ratingsResults.ToList()
            };

            _messagePublisher.Publish(ratingsGeneratedCommand, Guid.NewGuid());
        }
    }
}
