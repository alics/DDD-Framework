using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Queries
{
    public class PagedCollectionQueryResult<T> : IQueryResult
    {
        public PagedCollectionQueryResult(int pageNo, int pageSize, int totalItems, List<T> items)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            TotalItems = totalItems;
            Items = items;
        }
        public int PageNo { get;  }
        public int PageSize { get;  }
        public int TotalItems { get;  }
        public List<T> Items { get;  }
    }
}
