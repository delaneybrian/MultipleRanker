using System.Threading;

namespace MultipleRanker.Interfaces
{
    public interface IMessageSubscriber
    {
        void Start(CancellationToken cancellationToken);
    }
}
