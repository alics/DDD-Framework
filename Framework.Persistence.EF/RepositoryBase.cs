using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Persistence.EF
{
    public abstract class RepositoryBase<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        protected DbContext Context;
        protected DbSet<TEntity> DbSet;

        protected RepositoryBase(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual IList<TEntity> GetList(Expression<Func<TEntity, bool>> predict)
        {
            return GetQuery().Where(predict).ToList();

        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predict)
        {
            return await GetQuery().FirstAsync(predict);

        }

        public virtual async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predict)
        {
            return await GetQuery().Where(predict).ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await GetQuery().ToListAsync();
        }


        public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predict)
        {
            return await GetQuery().FirstOrDefaultAsync(predict);

        }

        public virtual async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predict)
        {
            var count = await GetQuery().CountAsync(predict);

            if (count > 0)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predict)
        {
            return await GetQuery().FirstOrDefaultAsync(predict);
        }

        public virtual TEntity Get(TKey id)
        {
            return GetQuery().FirstOrDefault(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id));
        }


        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            return await GetQuery().FirstOrDefaultAsync(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id));
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            var query = DbSet.AsQueryable();

            var includes = GetIncludes();

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    if (include.Body.NodeType == ExpressionType.Constant)
                    {
                        var memberExpression = include.Body as ConstantExpression;
                        query = query.Include(memberExpression.Value.ToString());
                    }
                    else
                    {
                        query = query.Include(include);
                    }
                }
            }

            return query;
        }

        protected abstract IList<Expression<Func<TEntity, object>>> GetIncludes();
    }
}

