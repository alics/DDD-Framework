using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Events
{
    public class ActionHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        private Action<TEvent> _action;
        public ActionHandler(Action<TEvent> action)
        {
            _action = action;
        }

        public void Handle(TEvent @event)
        {
            _action(@event);
        }
    }
}
