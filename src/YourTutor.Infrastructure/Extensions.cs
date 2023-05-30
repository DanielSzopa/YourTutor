using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Abstractions.Email;
using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Abstractions.UserManager;
using YourTutor.Application.Settings;
using YourTutor.Application.Settings.Email;
using YourTutor.Core.Services.SignInManager;
using YourTutor.Infrastructure.Authentication;
using YourTutor.Infrastructure.Authorization;
using YourTutor.Infrastructure.Authorization.CanEditTutor;
using YourTutor.Infrastructure.Authorization.CanRemoveOffer;
using YourTutor.Infrastructure.DAL;
using YourTutor.Infrastructure.Email;
using YourTutor.Infrastructure.Logging;
using YourTutor.Infrastructure.Security;
using YourTutor.Infrastructure.Services;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthorizationPolicies()
                .AddAuthenticationExtension(configuration)
                .AddRepositories()
                .AddSeeders()
                .AddHttpContextAccessor()
                .AddScoped<ISignInManager, SignInManager>()
                .AddScoped<ISignOutManager, SignOutManager>()
                .AddScoped<IEmailSender, EmailSender>()
                .AddScoped<IClock, Clock>()
                .AddScoped<IHttpContextService, HttpContextService>()
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingHandler<,>))
                .AddSingleton<IHashService, HashService>()
                .AddHostedService<DatabaseInitializer>()
                .AddScoped<IAuthorizationHandler, CanRemoveOfferRequirementHandler>()
                .AddScoped<IAuthorizationHandler, CanEditTutorRequirementHandler>()
                .RegisterAllSettings(configuration)
                .AddYourTutorDbContext(configuration);

            return services;
        }

        private static IServiceCollection RegisterAllSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterSettings<IdentitySettings>(configuration)
                .RegisterSettings<ConnectionStringsSettings>(configuration)
                .RegisterSettings<SendGridSettings>(configuration)
                .RegisterSettings<EmailSettings>(configuration)
                .RegisterSettings<SeederSettings>(configuration)
                .RegisterSettings<DbInitializerSettings>(configuration);

            return services;
        }

        public static ConfigureHostBuilder UseLogger(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            return host;
        }

    }
}


