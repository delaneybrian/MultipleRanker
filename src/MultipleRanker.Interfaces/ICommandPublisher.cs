using System.Threading.Tasks;
using MediatR;

namespace MultipleRanker.Interfaces
{
    public interface ICommandPublisher
    {
        Task Publish(IRequest cmd);
    }
}
