using Microsoft.EntityFrameworkCore;
using YourTutor.Application.Settings;
using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Helpers;

public class TestYourTutorDbContext
{
    internal YourTutorDbContext DbContext { get; }
    public TestYourTutorDbContext()
    {
        var connectionString = SettingsHelper.GetSettings<ConnectionStringsSettings>();
        var builder = new DbContextOptionsBuilder<YourTutorDbContext>();
        var options = builder.UseSqlServer(connectionString.DefaultConnectionString).Options;
        DbContext = new YourTutorDbContext(options);
    }
}
