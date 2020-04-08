using System;
using System.Collections.Generic;
using MediatR;

namespace MultipleRanker.Domain
{
    public abstract class AggregateBase
    {
        private readonly Dictionary<Type, Action<IRequest>> _eventHandlers = new Dictionary<Type, Action<IRequest>>();

        public void ApplyEvent(IRequest evt)
        {
            var eventType = evt.GetType();

            if (_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType](evt);
            }
        }

        protected void RegisterHandler<T>(Action<T> handler) where T : class
        {
            _eventHandlers.Add(typeof(T), o => handler(o as T));
        }
    }
}
