using Microsoft.Data.SqlClient;
using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Setup;

internal class TestDatabase
{
    internal YourTutorDbContext YourTutorDbContext { get; }
    private readonly string _connectionString;
    internal TestDatabase(string connectionString)
    {
        _connectionString = connectionString;
        var builder = new DbContextOptionsBuilder<YourTutorDbContext>();
        var options = builder.UseSqlServer(connectionString,
            x => x.MigrationsHistoryTable(ConstantsDAL.MigrationsHistoryTable, ConstantsDAL.DefaultSchema))
            .Options;
        YourTutorDbContext = new YourTutorDbContext(options);
    }

    internal async Task InitializeDbAsync()
    {
        if (!await YourTutorDbContext.Database.CanConnectAsync())
        {
            SqlConnectionStringBuilder connstrBldr = new SqlConnectionStringBuilder(_connectionString);
            connstrBldr.InitialCatalog = "master";

            using (SqlConnection conn = new(connstrBldr.ConnectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = 120;
                cmd.CommandText = "CREATE DATABASE [" + YourTutorDbContext.Database.GetDbConnection().Database + "] (EDITION = 'Standard')";
                await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();
            }
        }

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
