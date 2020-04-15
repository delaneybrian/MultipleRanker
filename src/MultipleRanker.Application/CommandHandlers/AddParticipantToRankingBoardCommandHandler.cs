using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Definitions.Commands;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.CommandHandlers
{
    public class AddParticipantToRankingBoardCommandHandler : AsyncRequestHandler<AddParticipantToRankingBoardCommand>
    {
        private readonly IRankingBoardSnapshotRepository _rankingBoardSnapshotRepository;

        public AddParticipantToRankingBoardCommandHandler(IRankingBoardSnapshotRepository rankingBoardSnapshotRepository)
        {
            _rankingBoardSnapshotRepository = rankingBoardSnapshotRepository;
        }

        protected override async Task Handle(AddParticipantToRankingBoardCommand cmd, CancellationToken cancellationToken)
        {
            try
            {
                var rankingBoardSnapshot = await _rankingBoardSnapshotRepository.Get(cmd.RankingBoardId);

                var rankingBoardModel = RatingBoardModel.For(rankingBoardSnapshot);

                rankingBoardModel.Apply(cmd);

                var updatedSnapshot = rankingBoardModel.ToSnapshot();

                await _rankingBoardSnapshotRepository.Set(updatedSnapshot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
