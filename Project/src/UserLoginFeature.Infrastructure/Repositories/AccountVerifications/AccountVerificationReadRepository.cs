using Persistence.Repositories;
using UserLoginFeature.Application.Abstractions.Repositories.AccountVerifications;
using UserLoginFeature.Domain.Entities.Identity;
using UserLoginFeature.Infrastructure.Contexts;

namespace UserLoginFeature.Infrastructure.Repositories.AccountVerifications
{
    public class AccountVerificationReadRepository : EfReadRepositoryBase<AccountVerification, BaseDbContext>, IAccountVerificationReadRepository
    {
        public AccountVerificationReadRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
