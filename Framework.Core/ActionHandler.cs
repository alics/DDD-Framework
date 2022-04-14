using System;
using System.Threading.Tasks;

namespace Framework.Core
{
    // Simple Event Handler For inline subscription
    public class ActionHandler< TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        private readonly Func<TEvent, Task> handler;

        public ActionHandler(Func<TEvent, Task> handlerDelegate)
        {
            handler = handlerDelegate;
        }

        public async Task HandleAsync(TEvent eventToHandle)
        {
            await handler(eventToHandle);
        }
    }

}
