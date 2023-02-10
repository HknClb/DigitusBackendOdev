using Microsoft.AspNetCore.Identity;
using UserLoginFeature.Application.Abstractions.Repositories.AccountVerifications;
using UserLoginFeature.Application.Exceptions;
using UserLoginFeature.Domain.Constants;
using UserLoginFeature.Domain.Entities.Identity;

namespace UserLoginFeature.Infrastructure.Security
{
    public class VerificationCodeManager<TUser> where TUser : class
    {
        private readonly IAccountVerificationReadRepository _verificationReadRepository;
        private readonly IAccountVerificationWriteRepository _verificationWriteRepository;
        public VerificationCodeManager(IAccountVerificationReadRepository verificationReadRepository, IAccountVerificationWriteRepository verificationWriteRepository)
        {
            _verificationReadRepository = verificationReadRepository;
            _verificationWriteRepository = verificationWriteRepository;
        }

        public async Task GenerateAsync(UserManager<TUser> manager, TUser user, string token, VerificationCodeTypes type)
        {
            string userId = await manager.GetUserIdAsync(user);

            AccountVerification? verification = await _verificationReadRepository.GetAsync(
                x => x.ConfirmationType == type && x.UserId == userId && x.IsActive);
            if (verification is null)
                verification = await _verificationWriteRepository.AddAsync(new()
                {
                    UserId = userId,
                    Token = token,
                    ConfirmationType = type
                });
            else
            {
                verification.Token = token;
                verification = await _verificationWriteRepository.UpdateAsync(verification);
            }
        }

        public async Task ValidateAsync(UserManager<TUser> manager, TUser user, string token, VerificationCodeTypes type)
        {
            string userId = await manager.GetUserIdAsync(user);
            AccountVerification? verification = await _verificationReadRepository.GetAsync(
                x => x.UserId == userId && x.ConfirmationType == type && x.Token == token && x.IsActive);
            if (verification is null)
                throw new BusinessException("Verification Code not found");
            verification.IsActive = false;
            await _verificationWriteRepository.UpdateAsync(verification);
        }
    }
}
