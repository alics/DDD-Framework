using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Queries
{
    public interface IQueryHandler<TQueryFilter, TQueryResult>
        where TQueryFilter : IQueryFilter
        where TQueryResult : IQueryResult
    {
        TQueryResult Handle(TQueryFilter filter);
    }
}
