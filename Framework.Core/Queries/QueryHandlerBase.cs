using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Core.Queries
{
    public abstract class QueryHandlerBase<TEntity> 
    {
        protected QueryHandlerBase()
        {
            SortFunctions=new Dictionary<string, Expression<Func<TEntity, object>>>();
            InitializeSortFunctions();
        }

        protected Dictionary<string, Expression<Func<TEntity, object>>> SortFunctions;
        protected abstract void InitializeSortFunctions();

    }
}