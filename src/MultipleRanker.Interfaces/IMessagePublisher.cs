using System;

namespace MultipleRanker.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish<T>(T content, Guid correlationId) where T : class;
    }
}
