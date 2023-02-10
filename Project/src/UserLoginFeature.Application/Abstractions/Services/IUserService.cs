using Microsoft.AspNetCore.Identity;
using UserLoginFeature.Application.Features.Users.Dtos;

namespace Application.Abstractions.Services.Users
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(CreateUserDto createUser);
    }
}
