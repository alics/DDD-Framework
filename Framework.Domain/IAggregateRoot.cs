using System.Collections.Generic;

namespace Framework.Domain
{
    public interface IAggregateRoot
    {
        IList<DomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}
