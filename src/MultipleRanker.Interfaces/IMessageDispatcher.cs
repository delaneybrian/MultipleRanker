using System.Threading.Tasks;

namespace MultipleRanker.Interfaces
{
    public interface IMessageDispatcher
    {
        Task DispatchMessage<T>(T cmd);
    }
}
