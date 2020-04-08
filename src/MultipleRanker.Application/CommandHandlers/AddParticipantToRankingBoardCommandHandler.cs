using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Application.CommandHandlers
{
    public class AddParticipantToRankingBoardCommandHandler : AsyncRequestHandler<AddParticipantToRankingBoardCommand>

    {
        protected override async Task Handle(AddParticipantToRankingBoardCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
