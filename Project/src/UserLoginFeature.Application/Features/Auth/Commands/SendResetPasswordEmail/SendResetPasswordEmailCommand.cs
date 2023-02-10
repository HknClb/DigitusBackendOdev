using MediatR;
using UserLoginFeature.Application.Abstractions.Services;

namespace UserLoginFeature.Application.Features.Auth.Commands.SendResetPasswordEmail
{
    public class SendResetPasswordEmailCommand : IRequest
    {
        public string Email { get; set; } = null!;

        public class SendResetPasswordEmailCommandHanlder : IRequestHandler<SendResetPasswordEmailCommand>
        {
            private readonly IAuthService _authService;

            public SendResetPasswordEmailCommandHanlder(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<Unit> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
            {
                await _authService.SendResetPasswordEmailAsync(request.Email);
                return Unit.Value;
            }
        }
    }
}