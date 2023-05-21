using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Helpers;

internal static class TestDbContext
{
    internal static YourTutorDbContext GetDbContext(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateAsyncScope();
        var db =  scope.ServiceProvider.GetRequiredService<YourTutorDbContext>();
        return db;
    }
}
