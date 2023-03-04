using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace YourTutor.Infrastructure.DAL
{
    internal static class Extensions
    {
        internal static IServiceCollection AddYourTutorDbContext(this IServiceCollection services)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YourTutorDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            services.AddDbContext<YourTutorDbContext>(x => x.UseSqlServer(connectionString));

            return services;
        }

        internal static async Task UpdateDbMigrations(this IServiceProvider serviceProvider)
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


