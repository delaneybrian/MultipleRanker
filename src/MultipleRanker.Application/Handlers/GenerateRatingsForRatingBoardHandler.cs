using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Domain.Raters;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class GenerateRatingsForRatingBoardHandler : IHandler<GenerateRatingsForRatingBoard>
    {
        private readonly IEnumerable<IRater> _raters;
        private readonly IRatingBoardSnapshotRepository _ratingBoardSnapshotRepository;
        private readonly IMessagePublisher _messagePublisher;

        public GenerateRatingsForRatingBoardHandler(IEnumerable<IRater> raters,
            IRatingBoardSnapshotRepository ratingBoardSnapshotRepository,
            IMessagePublisher messagePublisher)
        {
            _raters = raters;
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task HandleAsync(GenerateRatingsForRatingBoard evt)
        {
            var ratingBoardSnapshot = await _ratingBoardSnapshotRepository.Get(evt.RatingBoardId);

            var ratingBoardModel = RatingBoardModel.For(ratingBoardSnapshot);

            ratingBoardModel.Apply(evt);

            var rater = _raters.Single(r => r.IsFor(evt.RatingType.ToRankerType()));

            var ratingsResults = rater.Rate(ratingBoardModel);

            var ratingsGeneratedCommand = new RatingsGenerated
            {
                RatingId = Guid.NewGuid(),
                CalculatedAtUtc = DateTime.UtcNow,
                RatingBoardId = evt.RatingBoardId,
                RatingType = evt.RatingType,
                ParticipantRatings = ratingsResults.ToList()
            };

            _messagePublisher.Publish(ratingsGeneratedCommand, Guid.NewGuid());
        }
    }
}
