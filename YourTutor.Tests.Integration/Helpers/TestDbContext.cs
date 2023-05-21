using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Helpers;

internal static class TestDbContext
{
    internal static YourTutorDbContext DbContext(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateAsyncScope();
        var db =  scope.ServiceProvider.GetRequiredService<YourTutorDbContext>();
        return db;
    }
}
