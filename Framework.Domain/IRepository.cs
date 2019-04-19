using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Domain
{
    public interface IRepository
    {
    }

    public interface IRepository<Tkey,TEntity> : IRepository
        where TEntity : AggregateRoot<Tkey, TEntity>
    {
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        IList<TEntity> GetAll();

        void Create(TEntity aggregate);

        void Delete(TEntity aggregate);
    }
}
