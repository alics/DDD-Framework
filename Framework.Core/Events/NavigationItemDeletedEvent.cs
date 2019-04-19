using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Events
{
    public class NavigationItemDeletedEvent :IEvent
    {
        public NavigationItemDeletedEvent(object itemToBeDeleted)
        {
            this.ItemToBeDeleted = itemToBeDeleted;
        }

        public object ItemToBeDeleted { get; private set; }
    }
}
