using YourTutor.Tests.Integration.Helpers.Fixtures;
using YourTutor.Tests.Integration.Setup;
using YourTutor.Tests.Integration.Setup.Authentication;

namespace YourTutor.Tests.Integration.Controllers;

public class TutorControllerTests : ControllerTests, IAsyncLifetime
{
    public TutorControllerTests(YourTutorApp app, FakerFixture faker) : base(app, faker)
    {
    }

    private readonly string _tutorPath = "/tutor";
    private readonly string _errorPath = "/home/error";

    [Fact]
    public async Task MyAccount_WhenUserCanNotBeDetermined_Should_Return302Redirect_And_SetErrorToLocation()
    {
        //arrange
        AuthClient.AddUserIdClaimHeader(Guid.Empty.ToString());

        //act
        var response = await AuthClient.GetAsync(_tutorPath);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location.Should().Be(_errorPath);
    }

    public async Task DisposeAsync()
    {
        AuthClient.CleanClaimHeaders();
        await ResetDb();
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
