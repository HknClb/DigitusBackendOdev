using Microsoft.EntityFrameworkCore;
using UserLoginFeature.Application.Abstractions.Repositories;
using UserLoginFeature.Domain.Entities.Common;

namespace Persistence.Repositories
{
    public class EfWriteRepositoryBase<TEntity, TContext> : IWriteRepository<TEntity> where TEntity : Entity where TContext : DbContext
    {
        protected TContext Context { get; }

        public EfWriteRepositoryBase(TContext context)
        {
            Context = context;
        }

        public DbSet<TEntity> Table => Context.Set<TEntity>();

        public TEntity Add(TEntity entity)
        {
            Table.Add(entity);
            SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public void AddRange(IList<TEntity> entities)
        {
            Table.AddRangeAsync(entities);
            SaveChanges();
        }

        public async Task AddRangeAsync(IList<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            Table.Update(entity);
            SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Table.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Table.Remove(entity);
            SaveChanges();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Table.Remove(entity);
            await SaveChangesAsync();
            return entity;
        }

        public TEntity? DeleteById(string id)
        {
            TEntity? entity = Table.Find(Guid.Parse(id));
            if (entity is not null)
                Table.Remove(entity);
            SaveChanges();
            return entity;
        }

        public async Task<TEntity?> DeleteByIdAsync(string id)
        {
            TEntity? entity = await Table.FindAsync(Guid.Parse(id));
            if (entity is not null)
                Table.Remove(entity);
            await SaveChangesAsync();
            return entity;
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            Table.RemoveRange(entities);
            SaveChanges();
        }

        public async Task DeleteRangeAsync(IList<TEntity> entities)
        {
            Table.RemoveRange(entities);
            await SaveChangesAsync();
        }

        public int SaveChanges()
            => Context.SaveChanges();

        public int SaveChanges(bool acceptAllChangesOnSuccess)
            => Context.SaveChanges(acceptAllChangesOnSuccess);

        public async Task<int> SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await Context.SaveChangesAsync(cancellationToken);

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            => await Context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
