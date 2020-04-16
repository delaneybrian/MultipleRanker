using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Application.MessageHandlers
{
    public class AddParticipantToRatingBoardHandler : AsyncRequestHandler<AddParticipantToRatingBoard>
    {
        private readonly IRatingBoardSnapshotRepository _ratingBoardSnapshotRepository;

        public AddParticipantToRatingBoardHandler(IRatingBoardSnapshotRepository ratingBoardSnapshotRepository)
        {
            _ratingBoardSnapshotRepository = ratingBoardSnapshotRepository;
        }

        protected override async Task Handle(AddParticipantToRatingBoard cmd, CancellationToken cancellationToken)
        {
            try
            {
                var ratingBoardSnapshot = await _ratingBoardSnapshotRepository.Get(cmd.RankingBoardId);

                var ratingBoardModel = RatingBoardModel.For(ratingBoardSnapshot);

                ratingBoardModel.Apply(cmd);

                var updatedSnapshot = ratingBoardModel.ToSnapshot();

                await _ratingBoardSnapshotRepository.Set(updatedSnapshot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
