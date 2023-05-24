using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Settings;
using YourTutor.Application.ViewModels;
using YourTutor.Tests.Integration.Helpers;
using YourTutor.Tests.Integration.Helpers.Fixtures;
using YourTutor.Tests.Integration.Setup;
using YourTutor.Tests.Integration.TestFactories;

namespace YourTutor.Tests.Integration.Controllers;

public class AccountControllerTests : ControllerTests, IAsyncLifetime
{   
    private readonly string _identityCookie = SettingsHelper.GetSettings<IdentitySettings>().CookieName;

    private readonly string _registerPath = "/account/register";
    private readonly string _loginPath = "/account/login";
    private readonly string _logoutPath = "/account/logout";
    private readonly string _homePath = "/";

    private readonly Func<IHashService> _hashService;

    public AccountControllerTests(YourTutorApp app, FakerFixture faker) : base(app, faker)
    {
        _hashService = app.GetRequiredService<IHashService>;
    }
   
    #region Register

    [Fact]
    public async Task Register_WithValidVm_Should_AddUserToDbWithHashedPassword_And_Return302Redirect_And_SetLocationHeader_And_SetIdentityCookie()
    {
        //arrange
        var vm = ViewModelFactory.ValidRegisterVm;
        var formContent = vm.ToFormContent();       

        //act
        var response = await Client.PostAsync(_registerPath, formContent);

        //assert
        var user = await Db.Users
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
        var response = await Client.PostAsync(_registerPath, formContent);

        //assert
        var result = await Db.Users.AnyAsync();
      
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
        await Client.PostAsync(_registerPath, formContent);

        //assert
        var hashedPassword = await Db.Users
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

        await Db.Users.AddAsync(user);
        await Db.SaveChangesAsync();

        var formContent = vm.ToFormContent();

        //act
        var response = await Client.PostAsync(_registerPath, formContent);

        //assert
        var result = await Db.Users.CountAsync(x => x.Email == user.Email);

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

        await Db.Users.AddAsync(testUser.UserWithHashedPassword);
        await Db.SaveChangesAsync();

        var formContent = vm.ToFormContent();
        var datetime = DateTime.UtcNow;

        //act
        var response = await Client.PostAsync(_loginPath, formContent);

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
            Password = Faker.Random.String2(8),
        };

        await Db.Users.AddAsync(testUser.UserWithHashedPassword);
        await Db.SaveChangesAsync();

        var formContent = vm.ToFormContent();

        //act
        var response = await Client.PostAsync(_loginPath, formContent);

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
            Email = Faker.Person.Email,
            Password = Faker.Random.String2(8),
        };

        var formContent = vm.ToFormContent();

        //act
        var response = await Client.PostAsync(_loginPath, formContent);

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

        await Db.Users.AddAsync(testUser.UserWithHashedPassword);
        await Db.SaveChangesAsync();

        var formContent = vm.ToFormContent();

        //act
        var loginResponse = await Client.PostAsync(_loginPath, formContent);
        var logoutResponse = await Client.GetAsync(_logoutPath);

        //arrange
        using var scope = new AssertionScope();
        loginResponse.ContainsCookie(_identityCookie).Should().BeTrue();
        logoutResponse.ContainsCookie(_identityCookie).Should().BeFalse();
    }

    #endregion

    public Task DisposeAsync() => ResetDb();

    public Task InitializeAsync() => Task.CompletedTask;
}
