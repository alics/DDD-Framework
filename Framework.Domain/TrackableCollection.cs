using System.Collections.ObjectModel;
using System.Linq;
using Framework.Core.DependencyInjection;
using Framework.Core.Events;

namespace Framework.Domain
{
    public class TrackableCollection<T> : Collection<T>
    {
        protected override void RemoveItem(int index)
        {
            var itemToBeRemoved = Items[index];
            base.RemoveItem(index);

            var eventBus = ServiceLocator.Current.Resolve<IEventBus>();
            eventBus.Publish(new NavigationItemDeletedEvent(itemToBeRemoved));
        }

        protected override void ClearItems()
        {
            var queue = Items.Select(item => new NavigationItemDeletedEvent(item)).ToList();
            base.ClearItems();

            var eventBus = ServiceLocator.Current.Resolve<IEventBus>();

            foreach (var @event in queue)
            {
                eventBus.Publish(@event);
            }
        }
    }
}
