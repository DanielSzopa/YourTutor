namespace YourTutor.Tests.Integration.Controllers;

[Collection(nameof(YourTutorCollection))]
public sealed class HomeControllerTests
{
    private HttpClient _client;
    public HomeControllerTests(YourTutorApp app)
    {
        _client = app.Client;
    }


    [Fact]
    public async Task Test()
    {

    }

    [Fact]
    public async Task Test2()
    {

    }
}


