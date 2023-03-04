using Microsoft.AspNetCore.Mvc;
using YourTutor.Infrastructure.Constans;
using YourTutor.Infrastructure.Extensions;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Mvc.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration config)
        {
            var identitySettings = services.GetSettings<IdentitySettings>(config);

            services.AddAuthentication()
                .AddCookie(Schemes.IdentityScheme, options =>
                {
                    options.Cookie = new CookieBuilder()
                    {
                        Name = identitySettings.CookieName,
                        HttpOnly = true,
                        SecurePolicy = CookieSecurePolicy.Always
                    };
                    options.LoginPath = "/Account/Login";
                });

            return services;
        }

        internal static IServiceCollection AddControllersExtension(this IServiceCollection services)
        {
            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });

            return services;
        }
    }
}
