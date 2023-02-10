using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserLoginFeature.Domain.Entities.Common;
using UserLoginFeature.Domain.Entities.Identity;

namespace UserLoginFeature.Infrastructure.Contexts
{
    public class BaseDbContext : IdentityDbContext<User, Role, string>
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AccountVerification> AccountVerifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccountVerification>()
                .HasIndex(e => new { e.Token, e.UserId }).IsUnique();

            #region Default Account Initialize
            Role role = new()
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR".ToUpper()
            };

            builder.Entity<Role>().HasData(role);

            var hasher = new PasswordHasher<User>();

            User user = new()
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Name = "Digitus",
                Surname = "App",
                Email = "digitusapp@gmail.com",
                NormalizedEmail = "DIGITUSAPP@GMAIL.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = "QTC5PI3U6GFOVY2KSSYWA32ZYUMFBPF6",
                ConcurrencyStamp = "d8599b1a-40e3-40a5-a033-6624d2123d21",
                EmailConfirmed = true
            };
            user.PasswordHash = hasher.HashPassword(user, "DigitusApp");

            builder.Entity<User>().HasData(user);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );
            #endregion
        }

        private void Interceptor()
        {
            var entityEntries = ChangeTracker.Entries<Entity>();

            foreach (var entry in entityEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        break;
                }
            }
        }

        public override int SaveChanges()
        {
            Interceptor();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Interceptor();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Interceptor();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            Interceptor();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}