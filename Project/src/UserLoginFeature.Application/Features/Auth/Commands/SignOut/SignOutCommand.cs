using MediatR;
using UserLoginFeature.Application.Abstractions.Services;

namespace UserLoginFeature.Application.Features.Auth.Commands.SignOut
{
    public class SignOutCommand : IRequest
    {
        public class SignOutCommandHandler : IRequestHandler<SignOutCommand>
        {
            private readonly IAuthService _authService;

            public SignOutCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
            {
                await _authService.SignOutAsync();
                return Unit.Value;
            }
        }
    }
}
