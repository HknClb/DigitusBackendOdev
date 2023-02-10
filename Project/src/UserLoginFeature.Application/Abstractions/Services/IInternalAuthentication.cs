using UserLoginFeature.Application.Features.Auth.Dtos;

namespace UserLoginFeature.Application.Abstractions.Services
{
    public interface IInternalAuthentication
    {
        Task<SignedInDto> SignInAsync(string userNameOrEmail, string password);
        Task<SignedUpDto> SignUpAsync(SignUpUserDto user);
    }
}