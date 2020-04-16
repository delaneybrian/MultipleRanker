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

        public GenerateRatingsForRatingBoardHandler(IEnumerable<IRater> raters,
            IRatingBoardSnapshotRepository ratingBoardSnapshotRepository
            )
        {
            _raters = raters;
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
        }

        protected override async Task Handle(GenerateRatingsForRatingBoard cmd, CancellationToken cancellationToken)
        {
            var ratingBoardSnapshot = await _ratingBoardSnapshotRepository.Get(cmd.RatingBoardId);

            var ratingBoardModel = RatingBoardModel.For(ratingBoardSnapshot);

            ratingBoardModel.Apply(cmd);

            var rater = _raters.Single(r => r.IsFor(cmd.RatingType.ToRankerType()));

            var ratingsResults = rater.Rate(ratingBoardModel);

            //var ratingsGeneratedCommand = ratingsResults.ToCommand();

            //await _commandPublisher.Publish(ratingsGeneratedCommand);
        }
    }
}
