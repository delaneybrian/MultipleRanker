using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Application.CommandHandlers
{
    public class RatingsGeneratedCommandHandler : AsyncRequestHandler<RatingsGeneratedCommand>
    {
        protected override async Task Handle(RatingsGeneratedCommand request, CancellationToken cancellationToken)
        {
            //todo - decide what to do here!
        }
    }
}
