using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Application.CommandHandlers
{
    public class MatchUpCompletedCommandHandler : AsyncRequestHandler<MatchUpCompletedCommand>
    {
        protected override async Task Handle(MatchUpCompletedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
