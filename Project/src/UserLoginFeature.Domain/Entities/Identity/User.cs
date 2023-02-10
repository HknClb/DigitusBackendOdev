using Microsoft.AspNetCore.Identity;

namespace UserLoginFeature.Domain.Entities.Identity
{
    public class User : IdentityUser<string>
    {
        public User(string? userName = null) : base(userName)
        {
            Id = Guid.NewGuid().ToString();
        }

        public User(string userName, string email) : this(userName)
        {
            Email = email;
        }

        public User(string name, string surname, string userName, string email) : this(userName, email)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public virtual ICollection<AccountVerification> AccountVerifications { get; } = new List<AccountVerification>();
    }
}
