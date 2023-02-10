using Application.Abstractions.Services.Users;
using Microsoft.AspNetCore.Identity;
using UserLoginFeature.Application.Features.Users.Dtos;
using UserLoginFeature.Domain.Entities.Identity;

namespace Persistence.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserDto createUser)
        {
            User user = new(createUser.Name, createUser.Surname, createUser.UserName, createUser.Email)
            {
                EmailConfirmed = createUser.EmailConfirmed,
                PhoneNumberConfirmed = createUser.PhoneNumberConfirmed
            };
            IdentityResult result = await _userManager.CreateAsync(user, createUser.Password);

            if (result.Succeeded && createUser.Roles is not null)
                await _userManager.AddToRolesAsync(user, createUser.Roles);

            return result;
        }
    }
}