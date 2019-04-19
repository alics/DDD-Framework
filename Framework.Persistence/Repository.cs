using Framework.Core;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Persistence
{
    public abstract class Repository<TKey,TEntity>: IRepository<TKey, TEntity>
        where TEntity : AggregateRoot<TKey, TEntity>
    {
        protected DbContext DbContext;

        protected DbSet<TEntity> EntitySet
        {
            get
            {
                var entitySet = this.DbContext.Set<TEntity>();
                return entitySet;
            }
        }

        protected DbQuery<TEntity> Query
        {
            get
            {
                var includes = GetIncludeExpressions();

                if (!includes.Any())
                {
                    return EntitySet;
                }

                var dbQuery = EntitySet.Include(includes.First());

                foreach (var includeExperssion in includes)
                {
                    dbQuery = dbQuery.Include(includeExperssion);
                }

                return dbQuery;

            }
        }

        protected Repository(IUnitOfWork unitOfWork)
        {
            DbContext = unitOfWork as DbContext;
        }

        public void Create(TEntity aggregate)
        {
            this.EntitySet.Add(aggregate);
        }

        public void Delete(TEntity aggregate)
        {
            this.EntitySet.Remove(aggregate);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = this.Query.First(predicate);
            return entity;
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        
        {
            var entity = this.Query.FirstOrDefault(predicate);
            return entity;
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            var result = this.Query.Any(predicate);
            return result;
        }

        public IList<TEntity> GetAll()
        {
            var entities = this.Query.ToList();
            return entities;
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = this.Query.Where(predicate).ToList();
            return entities;
        }

        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            return Query.Any(predicate);
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Query.Count(predicate);
        }

        protected abstract IEnumerable<string> GetIncludeExpressions();
    }
}
