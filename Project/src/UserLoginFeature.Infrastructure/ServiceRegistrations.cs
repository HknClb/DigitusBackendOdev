using Application.Abstractions.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Services.Users;
using UserLoginFeature.Application.Abstractions.Repositories.AccountVerifications;
using UserLoginFeature.Application.Abstractions.Services;
using UserLoginFeature.Domain.Entities.Identity;
using UserLoginFeature.Infrastructure.Contexts;
using UserLoginFeature.Infrastructure.Repositories.AccountVerifications;
using UserLoginFeature.Infrastructure.Security;
using UserLoginFeature.Infrastructure.Services;

namespace UserLoginFeature.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string conntectionString = configuration.GetConnectionString("DigitusDb") ?? throw new ArgumentNullException("Connection string is not configured");
            services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(conntectionString));
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                    new TokenProviderDescriptor(
                        typeof(CustomEmailConfirmationTokenProvider<User>)));
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            }).AddEntityFrameworkStores<BaseDbContext>().AddDefaultTokenProviders();
            services.AddTransient<CustomEmailConfirmationTokenProvider<User>>();

            services.AddScoped<IAccountVerificationReadRepository, AccountVerificationReadRepository>();
            services.AddScoped<IAccountVerificationWriteRepository, AccountVerificationWriteRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAccountVerificationStatsService, AccountVerificationStatsService>();
            services.AddScoped<VerificationCodeManager<User>>();

            return services;
        }
    }
}