using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YourTutor.Application.Helpers;
using YourTutor.Infrastructure.DAL.Seeds;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IYourTutorSeeder _seeder;
    private readonly DbInitializerSettings _dbInitSettings;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger,
        IYourTutorSeeder seeder, IOptions<DbInitializerSettings> dbInitSettings)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _seeder = seeder;
        _dbInitSettings = dbInitSettings.Value;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (!_dbInitSettings.IsEnabled)
            return;

        using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<YourTutorDbContext>();

        if (!dbContext.Database.IsRelational())
            return;

        _logger.LogInformation(AppLogEvent.DbInit, "Check migrations...");
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations != null && pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync();
            _logger.LogInformation(AppLogEvent.DbInit, "Migrations have been added to database");

            if (!await dbContext.Users.AnyAsync())
            {
                _logger.LogInformation(AppLogEvent.DbInit, "Database need to be seeded");
                _logger.LogInformation(AppLogEvent.DbInit, "Seeding...");
                var users = _seeder.GetSeedData();
                await dbContext.Users.AddRangeAsync(users);
                await dbContext.SaveChangesAsync();
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
