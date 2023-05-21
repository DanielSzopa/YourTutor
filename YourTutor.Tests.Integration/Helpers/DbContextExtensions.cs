using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Helpers;

internal static class DbContextExtensions
{
    internal static YourTutorDbContext GetDbContext(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateAsyncScope();
        var db =  scope.ServiceProvider.GetRequiredService<YourTutorDbContext>();
        return db;
    }
}
