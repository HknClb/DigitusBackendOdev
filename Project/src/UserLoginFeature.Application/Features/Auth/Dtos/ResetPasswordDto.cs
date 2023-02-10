using Microsoft.AspNetCore.Identity;

namespace UserLoginFeature.Application.Features.Auth.Dtos
{
    public class ResetPasswordDto
    {
        public IdentityResult Result { get; set; } = null!;
    }
}
