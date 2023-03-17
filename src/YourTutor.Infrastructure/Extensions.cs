using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Abstractions.Email;
using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Abstractions.UserManager;
using YourTutor.Application.Settings;
using YourTutor.Application.Settings.Email;
using YourTutor.Core.Services.SignInManager;
using YourTutor.Infrastructure.DAL;
using YourTutor.Infrastructure.Email;
using YourTutor.Infrastructure.Logging;
using YourTutor.Infrastructure.Security;
using YourTutor.Infrastructure.Services;

namespace YourTutor.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services      
                .AddRepositories()
                .AddScoped<ISignInManager, SignInManager>()
                .AddScoped<ISignOutManager, SignOutManager>()
                .AddScoped<IEmailSender, EmailSender>()
                .AddScoped<IClock, Clock>()
                .AddScoped<IHttpContextService, HttpContextService>()
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingHandler<,>))
                .AddSingleton<IHashService, HashService>()
                .AddHostedService<DatabaseInitializer>()
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
                .RegisterSettings<EmailSettings>(configuration);

            return services;
        }

    }
}


