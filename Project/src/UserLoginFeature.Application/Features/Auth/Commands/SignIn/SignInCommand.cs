using FluentValidation.Results;
using MediatR;
using UserLoginFeature.Application.Abstractions.Services;
using UserLoginFeature.Application.Features.Auth.Dtos;
using UserLoginFeature.Application.Requests;

namespace UserLoginFeature.Application.Features.Auth.Commands.SignIn
{
    public class SignInCommand : IRequest<SignedInDto>, IAddValidationErrorsToList<ValidationFailure>
    {
        public IList<ValidationFailure>? ValidationFailures { get; set; }
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;

        public class SignInCommandHandler : IRequestHandler<SignInCommand, SignedInDto>
        {
            private readonly IInternalAuthentication _authService;

            public SignInCommandHandler(IInternalAuthentication authService)
            {
                _authService = authService;
            }

            public async Task<SignedInDto> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                if (request.ValidationFailures?.Count > 0)
                    return new();
                SignedInDto signedInDto = await _authService.SignInAsync(request.UserNameOrEmail, request.Password);
                return signedInDto;
            }
        }
    }
}
