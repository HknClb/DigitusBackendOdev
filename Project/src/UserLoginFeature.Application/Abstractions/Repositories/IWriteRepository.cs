using UserLoginFeature.Domain.Entities.Common;

namespace UserLoginFeature.Application.Abstractions.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        void AddRange(IList<TEntity> entities);

        Task AddRangeAsync(IList<TEntity> entities);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        TEntity Delete(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        void DeleteRange(IList<TEntity> entities);

        Task DeleteRangeAsync(IList<TEntity> entities);

        TEntity? DeleteById(string id);

        Task<TEntity?> DeleteByIdAsync(string id);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
