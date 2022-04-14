using System.Collections.Generic;

namespace Framework.Domain
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {
        private List<DomainEvent> _domainEvents;

        public IList<DomainEvent> DomainEvents => _domainEvents;

        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
