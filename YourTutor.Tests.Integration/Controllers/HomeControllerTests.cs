namespace YourTutor.Tests.Integration.Controllers;

[Collection(nameof(YourTutorCollection))]
public sealed class HomeControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDbAsync;

    public HomeControllerTests(YourTutorApp app)
    {
        _client = app.Client;
        _resetDbAsync = app.ResetDbAsync;
    }

    public Task DisposeAsync() => _resetDbAsync();

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Test()
    {

    }

    [Fact]
    public async Task Test2()
    {

    }
}


