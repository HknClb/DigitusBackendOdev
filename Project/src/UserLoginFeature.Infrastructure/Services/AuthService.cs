using Application.Abstractions.Services.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Specialized;
using System.Text.Encodings.Web;
using System.Web;
using UserLoginFeature.Application.Abstractions.Services;
using UserLoginFeature.Application.Exceptions;
using UserLoginFeature.Application.Features.Auth.Dtos;
using UserLoginFeature.Application.Features.Users.Dtos;
using UserLoginFeature.Domain.Entities.Identity;
using UserLoginFeature.Infrastructure.Security;

namespace UserLoginFeature.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, IMailService mailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
            _mapper = mapper;
        }

        public async Task SendEmailConfirmationAsync(string id)
        {
            User? user = await _userManager.FindByIdAsync(id);
            if (user is null)
                throw new BusinessException("User is not found");

            string verificationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _mailService.SendMail(user.Email, EmailConfirmationConfigurations.Subject, EmailConfirmationConfigurations.GetEmailBody(verificationCode));
        }

        public async Task ConfirmEmailAsync(string userId, string code)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new BusinessException("Email couldn't confirmed");

            IdentityResult result = await _userManager.ConfirmEmailAsync(user!, code);
            if (!result.Succeeded)
                throw new BusinessException("Email couldn't confirmed");

            await _signInManager.SignInAsync(user, true);
        }

        public async Task SendResetPasswordEmailAsync(string email)
        {
            User? user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new BusinessException("User is not found");

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            NameValueCollection queryStringCollection = HttpUtility.ParseQueryString(string.Empty);
            queryStringCollection.Add("Email", user.Email);
            queryStringCollection.Add("Code", token);
            string callbackUrl = "https://localhost:7200/Auth/ConfirmResetPassword?" + queryStringCollection.ToString();
            _mailService.SendMail(
                user.Email,
                "Reset your password",
                EmailConfirmationConfigurations.GetEmailBody($"You can reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."));
        }

        public async Task<ResetPasswordDto> ResetPasswordAsync(string email, string token, string password)
        {
            User? user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new BusinessException("User is not found");

            IdentityResult result = await _userManager.ResetPasswordAsync(user, token, password);
            return new() { Result = result };
        }

        public async Task<SignedInDto> SignInAsync(string userNameOrEmail, string password)
        {
            SignedInDto signedInDto = new();
            User? user = (await _userManager.FindByNameAsync(userNameOrEmail)) ?? (await _userManager.FindByEmailAsync(userNameOrEmail));
            if (user is null)
            {
                signedInDto.Errors.Add("Invalid login attempt");
                return signedInDto;
            }

            if (!(await _userManager.CheckPasswordAsync(user, password)))
            {
                signedInDto.Errors.Add("Invalid login attempt");
                return signedInDto;
            }

            signedInDto.Id = user.Id;
            if (!user.EmailConfirmed && _userManager.Options.SignIn.RequireConfirmedEmail)
            {
                signedInDto.EmailConfirmationRequired = true;
                await SendEmailConfirmationAsync(user.Id);
                return signedInDto;
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            if (!result.Succeeded)
                signedInDto.Errors.Add("Invalid login attempt");

            return signedInDto;
        }

        public async Task<SignedUpDto> SignUpAsync(SignUpUserDto user)
        {
            SignedUpDto signedUpDto = new();

            CreateUserDto createUser = _mapper.Map<CreateUserDto>(user);
            IdentityResult result = await _userService.CreateUserAsync(createUser);

            signedUpDto.Result = result;
            if (result.Succeeded)
            {
                User createdUser = await _userManager.FindByEmailAsync(user.Email);
                signedUpDto.Id = createdUser.Id;
                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    signedUpDto.EmailConfirmationRequired = true;
                    await SendEmailConfirmationAsync(createdUser.Id);
                }
                else
                    await _signInManager.SignInAsync(createdUser, true);
            }

            return signedUpDto;
        }
        public async Task SignOutAsync()
            => await _signInManager.SignOutAsync();
    }
}