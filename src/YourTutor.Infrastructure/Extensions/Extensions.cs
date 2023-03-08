using Microsoft.AspNetCore.Builder;
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
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure.Extensions
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
                .AddYourTutorDbContext(configuration);

            return services;
        }
    }
}


