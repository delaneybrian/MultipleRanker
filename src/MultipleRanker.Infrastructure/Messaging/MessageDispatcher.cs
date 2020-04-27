using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultipleRanker.Interfaces;
using MultipleRanker.Messaging.Extensions;

namespace MultipleRanker.Infrastructure.Messaging
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IDictionary<Type, IHandler> _messageHandlers;

        public MessageDispatcher(IEnumerable<IHandler> messageHandlers)
        {
            _messageHandlers = messageHandlers.ToDictionaryKeyedByGenericTypeArguments(typeof(IHandler<>));
        }

        public async Task DispatchMessage<T>(T message)
        {
            var msgType = message.GetType();

            if (_messageHandlers.ContainsKey(msgType))
            {
                dynamic handler = _messageHandlers[msgType];

                await handler.HandleAsync((dynamic) message);
            }
        }
    }
}
