using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Application.Abstractions;
using YourTutor.Core.Abstractions;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Services.SignInManager;
using YourTutor.Infrastructure.DAL;
using YourTutor.Infrastructure.DAL.Repositories;
using YourTutor.Infrastructure.Email;
using YourTutor.Infrastructure.Services;
using YourTutor.Shared.Settings;

namespace YourTutor.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ISignInManager, SignInManager>()
                .AddScoped<ISignOutManager, SignOutManager>()
                .AddScoped<IEmailSender, EmailSender>()
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


