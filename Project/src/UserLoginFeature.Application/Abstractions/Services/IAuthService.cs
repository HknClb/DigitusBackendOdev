using UserLoginFeature.Application.Features.Auth.Dtos;

namespace UserLoginFeature.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {
        Task SendResetPasswordEmailAsync(string email);
        Task ConfirmEmailAsync(string userId, string code);
        Task SendEmailConfirmationAsync(string id);
        Task<ResetPasswordDto> ResetPasswordAsync(string email, string token, string password);
        Task SignOutAsync();
    }
}
