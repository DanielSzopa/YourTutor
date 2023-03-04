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
                .AddScoped<IEmailSender, EmailSender>()                           
                .AddYourTutorDbContext(configuration);

            return services;
        }

        public static async Task UseInfrastructure(this WebApplication webApplication)
        {
            await webApplication.UpdateDbMigrations();
        }
    }
}


