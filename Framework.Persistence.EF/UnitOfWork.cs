using Framework.Core;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Framework.Persistence.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext Context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly IOutboxMessageDetector _outboxMessageDetector;

        public UnitOfWork(DbContext context, IEventBus eventBus, IOutboxMessageDetector outboxMessageDetector)
        {
            Context = context;
            _outboxMessageDetector = outboxMessageDetector;
            _domainEventsDispatcher = new DomainEventsDispatcher(context, eventBus);
        }

        public async Task BeginTransaction()
        {
            if (Context.Database.CurrentTransaction == null)
            {
                await Context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            }
        }

        public virtual async Task AddOutboxMessage(OutboxMessage outboxMessage)
        {
            Context.Set<OutboxMessage>().Add(outboxMessage);
            await Context.SaveChangesAsync();
        }

        public virtual async Task SaveChangesAsync()
        {
            var outboxMessages = _outboxMessageDetector.GetOutboxMessages();
            Context.Set<OutboxMessage>().AddRange(outboxMessages);
            await Context.SaveChangesAsync();

            await _domainEventsDispatcher.DispatchEventsAsync();
        }

        public virtual async Task CommitAsync()
        {
            if (Context.Database.CurrentTransaction == null)
            {
                throw new InvalidOperationException("there is no external transaction");
            }

            await Context.Database.CurrentTransaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (Context.Database.CurrentTransaction != null)
            {
                await Context.Database.CurrentTransaction.RollbackAsync();
            }
        }
    }
}
