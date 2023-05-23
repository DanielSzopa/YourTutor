using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Settings;
using YourTutor.Application.ViewModels;
using YourTutor.Infrastructure.DAL;
using YourTutor.Tests.Integration.Helpers;

namespace YourTutor.Tests.Integration.Controllers;

[Collection(nameof(YourTutorCollection))]
public class AccountControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDb;
    private readonly YourTutorDbContext _db;
    private readonly IServiceProvider _serviceProvider;
    public AccountControllerTests(YourTutorApp app)
    {
        _client = app.Client;
        _resetDb = app.ResetDbAsync;
        _db = app.YourTutorDbContext;
        _serviceProvider = app.ServiceProvider;
    }

    private readonly string _identityCookie = SettingsHelper.GetSettings<IdentitySettings>().CookieName;

    private readonly string _registerEndpoint = "/account/register";

    private readonly RegisterVm _validRegisterVm = new()
    {
        Email = "phill@gmail.com",
        FirstName = "Phill",
        LastName = "Cash",
        Password = "Test123!",
        PasswordConfirmation = "Test123!",
    };

    private readonly RegisterVm _invalidRegisterVm = new()
    {
        Email = "",
        FirstName = "Phill",
        LastName = "Cash",
        Password = "Test123!",
        PasswordConfirmation = "Test123!",
    };

    [Fact]
    public async Task Register_WithValidVm_Should_AddUserToDb()
    {
        //arrange
        var formContent = _validRegisterVm.ToFormContent();

        //act
        await _client.PostAsync(_registerEndpoint, formContent);

        //assert
        var user = await _db.Users
            .Include(u => u.Tutor)
            .FirstOrDefaultAsync();

        using var scope = new AssertionScope();
        user.Should().NotBeNull();
        user.Email.Value.Should().Be(_validRegisterVm.Email);
        user.FirstName.Value.Should().Be(_validRegisterVm.FirstName);
        user.LastName.Value.Should().Be(_validRegisterVm.LastName);
        user.HashPassword.Value.Should().NotBeEmpty();
        user.Id.Value.Should().NotBe(Guid.Empty);
        user.Tutor.Should().NotBeNull();
    }

    [Fact]
    public async Task Register_WithValidVm_Should_Return302Found_SetLocationHeader_SetIdentityCookie()
    {
        //arrange
        var formContent = _validRegisterVm.ToFormContent();

        //act
        var response = await _client.PostAsync(_registerEndpoint, formContent);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Found);
        response.Headers.Location?.OriginalString.Should().Be("/");
        response.ContainsCookie(_identityCookie).Should().BeTrue();
    }

    [Fact]
    public async Task Register_WithInvalidVm_Should_Return200Ok_WithoutLocationHeader_WithoutIdentityCookie()
    {
        //arrange
        var formContent = _invalidRegisterVm.ToFormContent();

        //act
        var response = await _client.PostAsync(_registerEndpoint, formContent);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Location.Should().BeNull();
        response.ContainsCookie(_identityCookie).Should().BeFalse();
    }

    [Fact]
    public async Task Register_WithInvalidVm_Should_NotAddUserToDb()
    {
        //arrange
        var formContent = _invalidRegisterVm.ToFormContent();

        //act
        await _client.PostAsync(_registerEndpoint, formContent);

        //assert
        var result = await _db.Users.AnyAsync();

        result.Should().BeFalse();
    }

    [Fact]
    public async Task Register_WithValidVm_ShouldHashPassword()
    {
        //arrange
        var formContent = _validRegisterVm.ToFormContent();

        //act
        await _client.PostAsync(_registerEndpoint, formContent);

        //assert
        var hashedPassword = await _db.Users
            .Select(u => u.HashPassword)
            .FirstOrDefaultAsync();

        var verifyResult = GetHashService().VerifyPassword(_validRegisterVm.Password, hashedPassword);

        verifyResult.Should().BeTrue();
    }


    private IHashService GetHashService()
        => _serviceProvider.GetRequiredService<IHashService>();
    public Task DisposeAsync() => _resetDb();

    public Task InitializeAsync() => Task.CompletedTask;
}
