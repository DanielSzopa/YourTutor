using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Helpers;
using YourTutor.Infrastructure.DAL.Seeds;

namespace YourTutor.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IYourTutorSeeder _seeder;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger,
        IYourTutorSeeder seeder)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _seeder = seeder;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<YourTutorDbContext>();

        if (!dbContext.Database.IsRelational())
        {
            _logger.LogInformation(AppLogEvent.DbInit, "Database isn't relational, migations haven't been added to database");
            return;
        }

        _logger.LogInformation(AppLogEvent.DbInit, "Check migrations...");
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
        if (pendingMigrations != null && pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation(AppLogEvent.DbInit, "Migrations have been added to database");

            if (!await dbContext.Users.AnyAsync(cancellationToken))
            {
                _logger.LogInformation(AppLogEvent.DbInit, "Database need to be seeded");
                _logger.LogInformation(AppLogEvent.DbInit, "Seeding...");
                var users = _seeder.GetSeedData();
                await dbContext.Users.AddRangeAsync(users, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation(AppLogEvent.DbInit, "Database has been seeded");
            }
        }
        else
        {
            _logger.LogInformation(AppLogEvent.DbInit, "Database is up to date, do not need add any migrations");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
