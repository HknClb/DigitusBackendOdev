using MediatR;
using UserLoginFeature.Application.Abstractions.Services;

namespace UserLoginFeature.Application.Features.Auth.Commands.SendEmailConfirmation
{
    public class SendEmailConfirmationCommand : IRequest
    {
        public string Id { get; set; } = null!;

        public class SendEmailConfirmationCommandHandler : IRequestHandler<SendEmailConfirmationCommand>
        {
            private readonly IAuthService _authService;

            public SendEmailConfirmationCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<Unit> Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
            {
                await _authService.SendEmailConfirmationAsync(request.Id);
                return Unit.Value;
            }
        }
    }
}
