using System.Threading.Tasks;
using MediatR;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Publishers
{
    public class MediatRCommandPublisher : ICommandPublisher
    {
        private readonly IMediator _mediator;

        public MediatRCommandPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(IRequest command)
        {
            await _mediator.Send(command);
        }
    }
}
