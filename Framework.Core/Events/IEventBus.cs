using System;
using System.Collections.Generic;

namespace Framework.Core.Events
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event)
            where TEvent: IEvent;

        void Subscribe<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : IEvent;
    }
}
