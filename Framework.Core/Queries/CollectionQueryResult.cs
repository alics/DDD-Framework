using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Queries
{
    public class CollectionQueryResult<T> : IQueryResult
    {
        public CollectionQueryResult(IEnumerable<T> items)
        {
            Items = items;
            TotalItems = items.Count();
        }

        public CollectionQueryResult(IEnumerable<T> items, int totalItems)
        {
            Items = items;
            TotalItems = totalItems;
        }

        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
