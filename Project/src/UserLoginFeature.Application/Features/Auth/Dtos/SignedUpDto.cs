using Microsoft.AspNetCore.Identity;

namespace UserLoginFeature.Application.Features.Auth.Dtos
{
    public class SignedUpDto
    {
        public string Id { get; set; } = null!;
        public bool EmailConfirmationRequired { get; set; } = false;
        public IdentityResult Result { get; set; } = null!;
    }
}