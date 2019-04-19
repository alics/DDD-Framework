using Framework.Core.DependencyInjection;
using Framework.Core.Events;

namespace Framework.Domain
{
    public class AggregateRoot<TKey, TEntity>: Entity<TKey, TEntity>
        where TEntity : Entity<TKey, TEntity>
    {
        protected IEventBus EventBus { get; } = ServiceLocator.Current.Resolve<IEventBus>();
    }
}
