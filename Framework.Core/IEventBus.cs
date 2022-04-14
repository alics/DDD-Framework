using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;
        Task PublishAsync<TEvent>(TEvent eventToPublish)
            where TEvent : IEvent;
    }
}
