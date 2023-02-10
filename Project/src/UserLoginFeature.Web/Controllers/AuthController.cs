using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UserLoginFeature.Application.Features.Auth.Commands.ConfirmEmail;
using UserLoginFeature.Application.Features.Auth.Commands.ResetPassword;
using UserLoginFeature.Application.Features.Auth.Commands.SendResetPasswordEmail;
using UserLoginFeature.Application.Features.Auth.Commands.SignIn;
using UserLoginFeature.Application.Features.Auth.Commands.SignOut;
using UserLoginFeature.Application.Features.Auth.Commands.SignUp;
using UserLoginFeature.Application.Features.Auth.Dtos;
using UserLoginFeature.Application.ViewModels;
using UserLoginFeature.Web.Controllers.Base;

namespace UserLoginFeature.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        [HttpGet("[action]")]
        public IActionResult SignIn([FromQuery] string? ReturnUrl = null)
        {
            AuthViewModel model = new() { SignIn = new(), ReturnUrl = ReturnUrl };
            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInAsync([FromForm] AuthViewModel model)
        {
            SignInCommand signInCommand = new() { UserNameOrEmail = model.SignIn!.Email, Password = model.SignIn.Password };
            SignedInDto response = await Mediator.Send(signInCommand);
            if (response.EmailConfirmationRequired)
            {
                TempData["UserId"] = response.Id;
                return RedirectToAction("ConfirmEmail");
            }
            if (response.Errors.Count > 0)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            return Redirect(model.ReturnUrl ?? "/");
        }

        [HttpGet("[action]")]
        public IActionResult SignUp([FromQuery] string? ReturnUrl = null)
        {
            AuthViewModel model = new() { SignUp = new(), ReturnUrl = ReturnUrl };
            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpAsync([FromForm] AuthViewModel model)
        {
            SignUpCommand signUpCommand = new()
            {
                Name = model.SignUp!.Name,
                Surname = model.SignUp.Surname,
                Email = model.SignUp!.Email,
                Password = model.SignUp.Password,
                ValidationFailures = new List<ValidationFailure>()
            };
            signUpCommand.ValidationFailures = new List<ValidationFailure>();
            SignedUpDto signedUpDto = await Mediator.Send(signUpCommand);
            if (signUpCommand.ValidationFailures?.Count > 0)
            {
                foreach (ValidationFailure failure in signUpCommand.ValidationFailures)
                    ModelState.AddModelError("", failure.ErrorMessage);
                return View(model);
            }

            if (signedUpDto.EmailConfirmationRequired)
            {
                TempData["UserId"] = signedUpDto.Id;
                return RedirectToAction("ConfirmEmail");
            }

            return Redirect(model.ReturnUrl ?? "/");
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOutAsync()
        {
            await Mediator.Send(new SignOutCommand());
            return Redirect("/");
        }

        [HttpGet("[action]")]
        public IActionResult ConfirmEmail([FromQuery] string? returnUrl)
        {
            AuthViewModel model = new() { ConfirmEmail = new(), ReturnUrl = returnUrl };
            string? userId = TempData["UserId"]?.ToString();
            if (userId is null)
                return Redirect("/");
            model.ConfirmEmail.Id = userId;
            return View(model);
        }

        [HttpPost("ConfirmEmail")]
        [ActionName("ConfirmEmail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmailAsync([FromForm] AuthViewModel model)
        {
            await Mediator.Send(new ConfirmEmailCommand() { Id = model.ConfirmEmail!.Id, Code = model.ConfirmEmail.Code });
            return Redirect(model.ReturnUrl ?? "/");
        }

        [HttpGet("[action]")]
        public IActionResult ForgotPassword()
        {
            AuthViewModel model = new() { ResetPassword = new() };
            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasswordAsync([FromForm] AuthViewModel model)
        {
            SendResetPasswordEmailCommand sendResetPasswordCommand = new() { Email = model.ResetPassword!.Email };
            await Mediator.Send(sendResetPasswordCommand);
            return View(model);
        }

        [HttpGet("[action]")]
        public IActionResult ConfirmResetPassword([FromQuery] string Email, [FromQuery] string Code)
        {
            AuthViewModel model = new() { ConfirmResetPassword = new(Email, Code) };
            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmResetPasswordAsync([FromForm] AuthViewModel model)
        {
            ResetPasswordCommand confirmResetPasswordCommand = new()
            {
                Email = model.ConfirmResetPassword!.Email,
                Code = model.ConfirmResetPassword.Code,
                Password = model.ConfirmResetPassword.Password,
                ConfirmPassword = model.ConfirmResetPassword.ConfirmPassword
            };
            ResetPasswordDto resetPasswordDto = await Mediator.Send(confirmResetPasswordCommand);
            if (!resetPasswordDto.Result.Succeeded)
            {
                foreach (var error in resetPasswordDto.Result.Errors)
                    ModelState.AddModelError("", $"[{error.Code}] {error.Description}");
                return View(model);
            }
            return RedirectToAction("SignIn");
        }
    }
}