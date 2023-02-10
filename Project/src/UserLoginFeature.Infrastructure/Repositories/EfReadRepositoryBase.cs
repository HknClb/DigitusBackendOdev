using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using UserLoginFeature.Application.Abstractions.Repositories;
using UserLoginFeature.Domain.Entities.Common;

namespace Persistence.Repositories
{
    public class EfReadRepositoryBase<TEntity, TContext> : IReadRepository<TEntity> where TEntity : Entity where TContext : DbContext
    {
        protected TContext Context { get; }

        public EfReadRepositoryBase(TContext context)
        {
            Context = context;
        }

        public DbSet<TEntity> Table => Context.Set<TEntity>();

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Table.AsQueryable();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            return queryable.FirstOrDefault(predicate);
        }

        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Table.AsQueryable();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            return queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Table.AsQueryable();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return orderBy(queryable).ToList();
            return queryable.ToList();
        }

        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Table.AsQueryable();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return await orderBy(queryable).ToListAsync(cancellationToken: cancellationToken);
            return await queryable.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}