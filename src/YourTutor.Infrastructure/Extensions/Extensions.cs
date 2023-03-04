using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Core.Abstractions;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Services.SignInManager;
using YourTutor.Infrastructure.DAL;
using YourTutor.Infrastructure.DAL.Repositories;
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
                .RegisterSettings<IdentitySettings>(configuration)
                .RegisterSettings<ConnectionStringsSettings>(configuration)
                .AddYourTutorDbContext(configuration);

            return services;
        }

        public static async Task UseInfrastructure(this WebApplication webApplication)
        {
            await webApplication.UpdateDbMigrations();
        }
    }
}


