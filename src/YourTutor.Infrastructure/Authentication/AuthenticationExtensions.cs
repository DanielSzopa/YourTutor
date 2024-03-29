﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Application.Settings;

namespace YourTutor.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration config)
    {
        var identitySettings = config.GetSettings<IdentitySettings>();

        services.AddAuthentication()
            .AddCookie(AuthenticationSchemes.IdentityScheme, options =>
            {
                options.Cookie = new CookieBuilder()
                {
                    Name = identitySettings.CookieName,
                    HttpOnly = true,
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessDenied";
            });

        return services;
    }
}


