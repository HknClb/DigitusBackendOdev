using Microsoft.AspNetCore.Identity;

namespace UserLoginFeature.Infrastructure.Security
{
    public class CustomEmailConfirmationTokenProvider<TUser>
                              : TotpSecurityStampBasedTokenProvider<TUser> where TUser : class
    {
        private readonly VerificationCodeManager<TUser> _verificationCodeManager;

        public CustomEmailConfirmationTokenProvider(VerificationCodeManager<TUser> verificationCodeManager)
        {
            _verificationCodeManager = verificationCodeManager;
        }

        public override async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            string token = await base.GenerateAsync(purpose, manager, user);
            await _verificationCodeManager.GenerateAsync(manager, user, token, Domain.Constants.VerificationCodeTypes.Email);
            return token;
        }

        public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
        {
            bool result = await base.ValidateAsync(purpose, token, manager, user);
            if (result)
                await _verificationCodeManager.ValidateAsync(manager, user, token, Domain.Constants.VerificationCodeTypes.Email);
            return result;
        }

        public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            var email = await manager.GetEmailAsync(user);

            return !string.IsNullOrWhiteSpace(email) && await manager.IsEmailConfirmedAsync(user);
        }
    }
}