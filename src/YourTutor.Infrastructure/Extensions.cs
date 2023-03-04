using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Core.Abstractions;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Services.SignInManager;
using YourTutor.Infrastructure.DAL;
using YourTutor.Infrastructure.DAL.Repositories;

namespace YourTutor.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddScoped<IUserRepository,UserRepository>()
                .AddScoped<ISignInManager, SignInManager>()
                .AddYourTutorDbContext();

            return services;
        }

        public static async Task UpdateDbMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<YourTutorDbContext>();

            if (dbContext.Database.IsRelational())
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
        }
    }
}


