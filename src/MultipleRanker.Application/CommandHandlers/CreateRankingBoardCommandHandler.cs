using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Definitions.Commands;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class CreateRankingBoardCommandHandler : AsyncRequestHandler<CreateRankingBoardCommand>
    {
        private readonly IRankingBoardSnapshotRepository _rankingBoardSnapshotRepository;

        public CreateRankingBoardCommandHandler(IRankingBoardSnapshotRepository rankingBoardSnapshotRepository)
        {
            _rankingBoardSnapshotRepository = rankingBoardSnapshotRepository;
        }

        protected override async Task Handle(CreateRankingBoardCommand command, CancellationToken cancellationToken)
        {
            var model = new RatingBoardModel();

            model.Apply(command);

            var snapshot = model.ToSnapshot();

            await _rankingBoardSnapshotRepository.Set(snapshot);
        }
    }
}
