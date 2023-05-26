using YourTutor.Tests.Integration.Helpers.Fixtures;
using YourTutor.Tests.Integration.Helpers.Repositories;
using YourTutor.Tests.Integration.Setup;
using YourTutor.Tests.Integration.Setup.Authentication;
using YourTutor.Tests.Integration.TestFactories;

namespace YourTutor.Tests.Integration.Controllers;

public class TutorControllerTests : ControllerTests, IAsyncLifetime
{
    private readonly TestUserRepository _userRepository;

    private readonly string _tutorPath = "/tutor";
    private readonly string _errorPath = "/home/error";
    private readonly string _offerPath = "/offer";

    public TutorControllerTests(YourTutorApp app, FakerFixture faker) : base(app, faker)
    {
        _userRepository = new TestUserRepository(Db);
    }

    [Fact]
    public async Task MyAccount_WhenTutorCanNotBeDetermined_Should_Return302Redirect_And_SetErrorToLocation()
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

    [Fact]
    public async Task MyAccount_WhenTutorExist_Should_Return200Ok()
    {
        //arrange
        var user = TestUserFactory.User;
        user.CreateTutor();
        await _userRepository.AddUserAsync(user);

        AuthClient.AddUserIdClaimHeader(user.Id.Value.ToString());

        //act
        var response = await AuthClient.GetAsync(_tutorPath);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Details_WhenTutorExist_Should_Return200Ok()
    {
        //arrange
        var user = TestUserFactory.User;
        user.CreateTutor();
        await _userRepository.AddUserAsync(user);

        //act
        var response = await Client.GetAsync($"{_tutorPath}/{user.Id.Value}");

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Details_WhenTutorDoesNotExist_Should_Return302Redirect_And_SetOfferToLocation()
    {
        //act
        var response = await Client.GetAsync($"{_tutorPath}/{Guid.NewGuid()}");

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location.Should().Be(_offerPath);
    }


    public async Task DisposeAsync()
    {
        AuthClient.CleanClaimHeaders();
        await ResetDb();
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
