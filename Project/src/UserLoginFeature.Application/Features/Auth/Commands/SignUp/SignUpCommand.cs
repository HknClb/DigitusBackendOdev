using FluentValidation.Results;
using MediatR;
using UserLoginFeature.Application.Abstractions.Services;
using UserLoginFeature.Application.Features.Auth.Dtos;
using UserLoginFeature.Application.Requests;

namespace UserLoginFeature.Application.Features.Auth.Commands.SignUp
{
    public class SignUpCommand : IRequest<SignedUpDto>, IAddValidationErrorsToList<ValidationFailure>
    {
        public IList<ValidationFailure>? ValidationFailures { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignedUpDto>
        {
            private readonly IInternalAuthentication _authService;

            public SignUpCommandHandler(IInternalAuthentication authService)
            {
                _authService = authService;
            }

            public async Task<SignedUpDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {
                return await _authService.SignUpAsync(new(request.Name, request.Surname, request.Email, request.Email, request.Password));
            }
        }
    }
}
