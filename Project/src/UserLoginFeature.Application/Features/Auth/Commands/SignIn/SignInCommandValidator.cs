using FluentValidation;

namespace UserLoginFeature.Application.Features.Auth.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(command => command.UserNameOrEmail).NotEmpty().NotNull();
            RuleFor(command => command.Password).NotEmpty().NotNull();
        }
    }
}
