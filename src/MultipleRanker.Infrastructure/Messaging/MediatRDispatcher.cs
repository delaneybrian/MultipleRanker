using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Messaging
{
    public class MediatRDispatcher : IMessageDispatcher
    {
        private readonly IMediator _mediator;

        public MediatRDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchMessage(IRequest command)
        {
            await _mediator.Send(command);
        }
    }
}
