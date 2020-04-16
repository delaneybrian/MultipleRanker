using System.Threading.Tasks;
using MediatR;

namespace MultipleRanker.Interfaces
{
    public interface IMessageDispatcher
    {
        Task DispatchMessage(IRequest cmd);
    }
}
