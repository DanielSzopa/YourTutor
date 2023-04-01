using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Helpers;

namespace YourTutor.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope =  _serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<YourTutorDbContext>();

        if (dbContext.Database.IsRelational())
        {
            _logger.LogInformation(AppLogEvent.DbInit ,"Check migrations...");
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations != null && pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
                _logger.LogInformation(AppLogEvent.DbInit, "Migrations have been added to database");
            }
            else
            {
                _logger.LogInformation(AppLogEvent.DbInit, "Database is up to date, do not need add any migrations");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
