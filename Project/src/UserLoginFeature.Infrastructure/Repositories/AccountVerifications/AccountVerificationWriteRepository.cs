using Persistence.Repositories;
using UserLoginFeature.Application.Abstractions.Repositories.AccountVerifications;
using UserLoginFeature.Domain.Entities.Identity;
using UserLoginFeature.Infrastructure.Contexts;

namespace UserLoginFeature.Infrastructure.Repositories.AccountVerifications
{
    public class AccountVerificationWriteRepository : EfWriteRepositoryBase<AccountVerification, BaseDbContext>, IAccountVerificationWriteRepository
    {
        public AccountVerificationWriteRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
