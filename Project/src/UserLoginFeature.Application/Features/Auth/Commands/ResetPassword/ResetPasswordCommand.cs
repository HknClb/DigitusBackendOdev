using MediatR;
using UserLoginFeature.Application.Abstractions.Services;
using UserLoginFeature.Application.Features.Auth.Dtos;

namespace UserLoginFeature.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ResetPasswordDto>
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;

        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordDto>
        {
            private readonly IAuthService _authService;

            public ResetPasswordCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<ResetPasswordDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                return await _authService.ResetPasswordAsync(request.Email, request.Code, request.Password);
            }
        }
    }
}
