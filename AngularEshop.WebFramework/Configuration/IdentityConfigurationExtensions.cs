using AngularEshop.Common;
using AngularEshop.Data.Context;
using AngularEshop.Entities.Access;
using AngularEshop.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AngularEshop.WebFramework.Configuration
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<User, Role>(identityOptions =>
                {
                    identityOptions.Password.RequireDigit = settings.PasswordRequiredDigit;
                    identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                    identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumeric;
                    identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;
                    identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;

                    identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}