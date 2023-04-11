using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Application.Settings;
using YourTutor.Infrastructure.Constans;

namespace YourTutor.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration config)
    {
        var identitySettings = config.GetSettings<IdentitySettings>();

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
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

        return services;
    }
}


