using Framework.Core;
using System;
using System.Runtime.CompilerServices;

namespace Framework.Domain
{
    public abstract class DomainEvent: IEvent
    {
        protected DomainEvent()
        {
            
        }

        protected DomainEvent(string aggregateId)
        {
            AggregateId = aggregateId;
            OccurredOn = DateTime.UtcNow;
        }

        public DateTime OccurredOn { get; private set; }

        public string AggregateId { get; set; }
    }
}
