using MediatR;
using UserLoginFeature.Application.Abstractions.Services;

namespace UserLoginFeature.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest
    {
        public string Id { get; set; } = null!;
        public string Code { get; set; } = null!;

        public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
        {
            private readonly IAuthService _authService;

            public ConfirmEmailCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                await _authService.ConfirmEmailAsync(request.Id, request.Code);
                return Unit.Value;
            }
        }
    }
}
