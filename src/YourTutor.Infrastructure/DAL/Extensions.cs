using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTutor.Infrastructure.Extensions;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure.DAL
{
    internal static class Extensions
    {
        internal static IServiceCollection AddYourTutorDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = services.GetSettings<ConnectionStringsSettings>(configuration);
            services.AddDbContext<YourTutorDbContext>(x => x.UseSqlServer(connectionString.DefaultConnectionString));

            return services;
        }

        internal static async Task UpdateDbMigrations(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateAsyncScope();
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


