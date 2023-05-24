using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Settings;
using YourTutor.Application.ViewModels;
using YourTutor.Infrastructure.DAL;
using YourTutor.Tests.Integration.Helpers;
using YourTutor.Tests.Integration.Helpers.Fixtures;
using YourTutor.Tests.Integration.Setup;
using YourTutor.Tests.Integration.TestFactories;

namespace YourTutor.Tests.Integration.Controllers;

[Collection(nameof(YourTutorCollection))]
public class AccountControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDb;
    private readonly Func<IHashService> _hashService;
    private readonly YourTutorDbContext _db;
    private readonly Faker _faker;
    public AccountControllerTests(YourTutorApp app, FakerFixture faker)
    {
        _client = app.Client;
        _resetDb = app.ResetDbAsync;
        _db = app.YourTutorDbContext;
        _faker = faker.Faker;
        _hashService = app.GetRequiredService<IHashService>;
    }

    private readonly string _identityCookie = SettingsHelper.GetSettings<IdentitySettings>().CookieName;

    private readonly string _registerPath = "/account/register";
    private readonly string _loginPath = "/account/login";
    private readonly string _logoutPath = "/account/logout";
    private readonly string _homePath = "/";

    #region Register

    [Fact]
    public async Task Register_WithValidVm_Should_AddUserToDbWithHashedPassword_And_Return302Redirect_And_SetLocationHeader_And_SetIdentityCookie()
    {
        //arrange
        var vm = ViewModelFactory.ValidRegisterVm;
        var formContent = vm.ToFormContent();       

        //act
        var response = await _client.PostAsync(_registerPath, formContent);

        //assert
        var user = await _db.Users
            .Include(u => u.Tutor)
            .FirstOrDefaultAsync();
        var verifyResult = _hashService().VerifyPassword(vm.Password, user.HashPassword);

        using var scope = new AssertionScope();
        user.Should().NotBeNull();
        user.Email.Value.Should().Be(vm.Email);
        user.FirstName.Value.Should().Be(vm.FirstName);
        user.LastName.Value.Should().Be(vm.LastName);
        user.Id.Value.Should().NotBe(Guid.Empty);
        user.Tutor.Should().NotBeNull();
        verifyResult.Should().BeTrue();

        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location?.OriginalString.Should().Be(_homePath);
        response.ContainsCookie(_identityCookie).Should().BeTrue();
    }

    [Fact]
    public async Task Register_WithInvalidVm_Should_DoNotAddUserToDb_And_Return200Ok_And_WithoutSetLocationHeader_And_WithoutIdentityCookie()
    {
        //arrange
        var formContent = ViewModelFactory.InvalidRegisterVm.ToFormContent();

        //act
        var response = await _client.PostAsync(_registerPath, formContent);

        //assert
        var result = await _db.Users.AnyAsync();
      
        using var scope = new AssertionScope();
        result.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Location.Should().BeNull();
        response.ContainsCookie(_identityCookie).Should().BeFalse();
    }

    [Fact]
    public async Task Register_WithValidVm_ShouldHashPassword()
    {
        //arrange
        var vm = ViewModelFactory.ValidRegisterVm;
        var formContent = vm.ToFormContent();

        //act
        await _client.PostAsync(_registerPath, formContent);

        //assert
        var hashedPassword = await _db.Users
            .Select(u => u.HashPassword)
            .FirstOrDefaultAsync();

        var verifyResult = _hashService().VerifyPassword(vm.Password, hashedPassword);

        verifyResult.Should().BeTrue();
    }

    [Fact]
    public async Task Register_WhenEmailIsAlreadyExist_ShouldNotAddUserToDb_Return200Ok_And_WithoutSetLocationHeader_And_WithoutIdentityCookie()
    {
        //arrange
        var user = TestUserFactory.User;
        var vm = new RegisterVm
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.HashPassword,
            PasswordConfirmation = user.HashPassword
        };

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        var formContent = vm.ToFormContent();

        //act
        var response = await _client.PostAsync(_registerPath, formContent);

        //assert
        var result = await _db.Users.CountAsync(x => x.Email == user.Email);

        using var scope = new AssertionScope();
        result.Should().Be(1);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Location.Should().BeNull();
        response.ContainsCookie(_identityCookie).Should().BeFalse();
    }

    #endregion

    #region Login

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Login_WhenCredentialsAreGood_WithDeterminedRememberMe_Should_Return302Redirect_And_SetLocationHeader_And_SetIdentityCookieWithAppropriateExpiration(bool isRememberMe)
    {
        //arrange        
        var testUser = TestUserFactory.GetTestUserWithHashing(_hashService());
        var vm = new LoginVm()
        {
            Email = testUser.UserWithHashedPassword.Email,
            Password = testUser.OrgiginalPassword,
            RememberMe = isRememberMe
        };

        await _db.Users.AddAsync(testUser.UserWithHashedPassword);
        await _db.SaveChangesAsync();

        var formContent = vm.ToFormContent();
        var datetime = DateTime.UtcNow;

        //act
        var response = await _client.PostAsync(_loginPath, formContent);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location.Should().Be(_homePath);
        response.ContainsCookie(_identityCookie).Should().BeTrue();

        var cookie = response.GetCookie(_identityCookie);
        if (isRememberMe)
        {
            var cookieDatetime = datetime.AddDays(SettingsHelper.GetSettings<IdentitySettings>().ExpiresDays);
            cookie.Expires.Should().BeCloseTo(cookieDatetime, TimeSpan.FromMinutes(1));
        }
        else
        {
            cookie.Expires.Should().BeBefore(new DateTime(0002, 01, 01));
        }
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ShouldReturn200Ok_And_WithoutSetLocation_And_WithoutSetIdentityCookie()
    {
        //arrange
        var testUser = TestUserFactory.GetTestUserWithHashing(_hashService());
        var vm = new LoginVm()
        {
            Email = testUser.UserWithHashedPassword.Email,
            Password = _faker.Random.String2(8),
        };

        await _db.Users.AddAsync(testUser.UserWithHashedPassword);
        await _db.SaveChangesAsync();

        var formContent = vm.ToFormContent();

        //act
        var response = await _client.PostAsync(_loginPath, formContent);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Location.Should().BeNull();
        response.ContainsCookie(_identityCookie).Should().BeFalse();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldReturn200Ok_And_WithoutSetLocation_And_WithoutSetIdentityCookie()
    {
        //arrange
        var vm = new LoginVm()
        {
            Email = _faker.Person.Email,
            Password = _faker.Random.String2(8),
        };

        var formContent = vm.ToFormContent();

        //act
        var response = await _client.PostAsync(_loginPath, formContent);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Location.Should().BeNull();
        response.ContainsCookie(_identityCookie).Should().BeFalse();
    }

    #endregion

    #region Logout

    [Fact]
    public async Task Logout_WhenCallLogout_ShouldReturnExpiredIdentityCookie()
    {
        //arrange        
        var testUser = TestUserFactory.GetTestUserWithHashing(_hashService());
        var vm = new LoginVm()
        {
            Email = testUser.UserWithHashedPassword.Email,
            Password = testUser.OrgiginalPassword,
            RememberMe = true
        };

        await _db.Users.AddAsync(testUser.UserWithHashedPassword);
        await _db.SaveChangesAsync();

        var formContent = vm.ToFormContent();

        //act
        var loginResponse = await _client.PostAsync(_loginPath, formContent);
        var logoutResponse = await _client.GetAsync(_logoutPath);

        //arrange
        using var scope = new AssertionScope();
        loginResponse.ContainsCookie(_identityCookie).Should().BeTrue();
        logoutResponse.ContainsCookie(_identityCookie).Should().BeFalse();
    }

    #endregion

    public Task DisposeAsync() => _resetDb();

    public Task InitializeAsync() => Task.CompletedTask;
}
