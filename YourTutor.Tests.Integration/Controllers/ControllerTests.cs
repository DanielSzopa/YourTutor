using YourTutor.Infrastructure.DAL;
using YourTutor.Tests.Integration.Helpers.Fixtures;
using YourTutor.Tests.Integration.Setup;

namespace YourTutor.Tests.Integration.Controllers;

[Collection(nameof(YourTutorCollection))]
public abstract class ControllerTests
{
    protected HttpClient Client { get; }
    protected HttpClient AuthClient { get; }
    protected Faker Faker { get; }
    internal YourTutorDbContext Db { get; }
    protected Func<Task> ResetDb { get; }
    protected ControllerTests(YourTutorApp app, FakerFixture faker)
    {
        Client = app.Client;
        AuthClient = app.AuthenticatedClient;
        Faker = faker.Faker;
        Db = app.TestDatabase.YourTutorDbContext;
        ResetDb = app.ResetDbAsync;
    }
}
