using Microsoft.EntityFrameworkCore;
using UserLoginFeature.Domain.Entities.Common;

namespace UserLoginFeature.Application.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        DbSet<TEntity> Table { get; }
    }
}
