using Framework.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Queries
{
    public class QueryBus : IQueryBus
    {
        public TQueryResult Dispatch<TQueryFilter, TQueryResult>(TQueryFilter filter)
            where TQueryFilter : IQueryFilter
            where TQueryResult : IQueryResult
        {
            var handler = ServiceLocator.Current.Resolve<IQueryHandler<TQueryFilter, TQueryResult>>();
            var result = handler.Handle(filter);
            return result;
        }
    }
}
