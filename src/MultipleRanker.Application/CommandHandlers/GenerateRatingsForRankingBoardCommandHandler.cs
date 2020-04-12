using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Definitions.Commands;
using MultipleRanker.Domain;
using MultipleRanker.Domain.Rankers;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class GenerateRatingsForRankingBoardCommandHandler : AsyncRequestHandler<GenerateRatingsForRankingBoardCommand>
    {
        private readonly IEnumerable<IRanker> _rankers;
        private readonly IRankingBoardSnapshotRepository _rankingBoardSnapshotRepository;
        private readonly ICommandPublisher _commandPublisher;

        public GenerateRatingsForRankingBoardCommandHandler(IEnumerable<IRanker> rankers,
            IRankingBoardSnapshotRepository rankingBoardSnapshotRepository,
            ICommandPublisher commandPublisher
            )
        {
            _rankers = rankers;
            _rankingBoardSnapshotRepository = rankingBoardSnapshotRepository;
            _commandPublisher = commandPublisher;
        }

        protected override async Task Handle(GenerateRatingsForRankingBoardCommand cmd, CancellationToken cancellationToken)
        {
            var rankingBoardSnapshot = await _rankingBoardSnapshotRepository.Get(cmd.RankingBoardId);

            var rankingBoardModel = RankingBoardModel.For(rankingBoardSnapshot);

            rankingBoardModel.Apply(cmd);

            var ranker = _rankers.Single(r => r.IsFor(cmd.RankerType.ToRankerType()));

            var ratingsResults = ranker.Rank(rankingBoardModel);

            var ratingsGeneratedCommand = ratingsResults.ToCommand();

            await _commandPublisher.Publish(ratingsGeneratedCommand);
        }
    }
}
