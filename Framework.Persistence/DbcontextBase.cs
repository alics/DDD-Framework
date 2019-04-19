using System;
using System.Data.Entity;
using Framework.Core.DependencyInjection;
using Framework.Core.Events;

namespace Framework.Persistence
{
    public class DbContextBase : DbContext, IEventHandler<NavigationItemDeletedEvent>, IDisposable
    {
        public DbContextBase(string connectionName) : base(connectionName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            if (ServiceLocator.Current != null)
            {
                var eventBus = ServiceLocator.Current.Resolve<IEventBus>();
                eventBus.Subscribe(this);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new RemoveUnderscoreForeignKeyNamingConvention());
        }

        public void Handle(NavigationItemDeletedEvent @event)
        {
            Entry(@event.ItemToBeDeleted).State = EntityState.Deleted;
        }
    }
}
