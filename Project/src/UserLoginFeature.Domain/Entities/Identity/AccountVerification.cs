using UserLoginFeature.Domain.Constants;
using UserLoginFeature.Domain.Entities.Common;

namespace UserLoginFeature.Domain.Entities.Identity
{
    public class AccountVerification : Entity
    {
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public VerificationCodeTypes ConfirmationType { get; set; }

        public virtual User User { get; set; } = null!;
    }
}