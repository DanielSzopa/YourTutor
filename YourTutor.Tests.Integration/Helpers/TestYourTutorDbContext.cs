using Microsoft.EntityFrameworkCore;
using YourTutor.Application.Settings;
using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Helpers;

internal class TestYourTutorDbContext
{
    internal YourTutorDbContext YourTutorDbContext { get; }
    internal TestYourTutorDbContext()
    {
        var connectionString = SettingsHelper.GetSettings<ConnectionStringsSettings>();
        var builder = new DbContextOptionsBuilder<YourTutorDbContext>();
        var options = builder.UseSqlServer(connectionString.DefaultConnectionString).Options;
        YourTutorDbContext = new YourTutorDbContext(options);
    }

    internal async Task InitializeDbAsync()
    {
        var pendingMigrations = await YourTutorDbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations != null && pendingMigrations.Any())
        {
            await YourTutorDbContext.Database.MigrateAsync();
        }
    }

    internal async Task CustomDisposeAsync()
    {
        await YourTutorDbContext.Database.EnsureDeletedAsync();
        await YourTutorDbContext.DisposeAsync();
    }
}
