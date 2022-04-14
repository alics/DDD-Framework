using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TQueryResult> Dispatch<TQueryFilter, TQueryResult>(TQueryFilter filter)
            where TQueryFilter : IQueryFilter
            where TQueryResult : IQueryResult
        {
            var handler = _serviceProvider.GetService<IQueryHandler<TQueryFilter, TQueryResult>>();
            var result = await handler.HandleAsync(filter);
            return result;
        }
    }
}
