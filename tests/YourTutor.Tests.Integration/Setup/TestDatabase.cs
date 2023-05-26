using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Setup;

internal class TestDatabase
{
    internal YourTutorDbContext YourTutorDbContext { get; }
    internal TestDatabase(string connectionString)
    {
        var builder = new DbContextOptionsBuilder<YourTutorDbContext>();
        var options = builder.UseSqlServer(connectionString,
            x => x.MigrationsHistoryTable(ConstantsDAL.MigrationsHistoryTable, ConstantsDAL.DefaultSchema))
            .Options;
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
