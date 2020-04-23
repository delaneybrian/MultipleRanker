using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Domain.Raters;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.MessageHandlers
{
    public class GenerateRatingsForRatingBoardHandler : AsyncRequestHandler<GenerateRatingsForRatingBoard>
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

        protected override async Task Handle(GenerateRatingsForRatingBoard cmd, CancellationToken cancellationToken)
        {
            var ratingBoardSnapshot = await _ratingBoardSnapshotRepository.Get(cmd.RatingBoardId);

            var ratingBoardModel = RatingBoardModel.For(ratingBoardSnapshot);

            ratingBoardModel.Apply(cmd);

            var rater = _raters.Single(r => r.IsFor(cmd.RatingType.ToRankerType()));

            var ratingsResults = rater.Rate(ratingBoardModel);

            var ratingsGeneratedCommand = new RatingsGenerated
            {
                RatingId = Guid.NewGuid(),
                CalculatedAtUtc = DateTime.UtcNow,
                RatingBoardId = cmd.RatingBoardId,
                RatingType = cmd.RatingType,
                ParticipantRatings = ratingsResults.ToList()
            };

            _messagePublisher.Publish(ratingsGeneratedCommand, Guid.NewGuid());
        }
    }
}
