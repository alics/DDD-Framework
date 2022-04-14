using Framework.Core;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Framework.Persistence.EF
{
    public class OutboxMessageDetector : IOutboxMessageDetector
    {
        private readonly DbContext _context;

        public OutboxMessageDetector(DbContext context)
        {
            _context = context;
        }

        public List<OutboxMessage> GetOutboxMessages()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var domainEventNotifications = domainEvents.Where(n => n is INotification);

            var outboxMessages = new List<OutboxMessage>();

            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = domainEventNotification.GetType().Name;
                var data = JsonConvert.SerializeObject(domainEventNotification);

                var outboxMessage = new OutboxMessage(
                    domainEventNotification.OccurredOn,
                    type,
                    data);

                outboxMessages.Add(outboxMessage);
            }

            return outboxMessages;
        }
    }

    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly DbContext _context;
        private readonly IEventBus _eventBus;

        public DomainEventsDispatcher( DbContext context, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _context = context;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _eventBus.PublishAsync(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
