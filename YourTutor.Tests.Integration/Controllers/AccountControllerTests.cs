﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using YourTutor.Application.ViewModels;
using YourTutor.Infrastructure.DAL;
using YourTutor.Tests.Integration.Helpers;

namespace YourTutor.Tests.Integration.Controllers;

[Collection(nameof(YourTutorCollection))]
public class AccountControllerTests : IClassFixture<TestYourTutorDbContext>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDb;
    private readonly YourTutorDbContext _db;
    public AccountControllerTests(YourTutorApp app, TestYourTutorDbContext db)
    {
        _client = app.Client;
        _resetDb = app.ResetDbAsync;
        var test = new TestYourTutorDbContext();
        _db = db.DbContext;
    }

    [Fact]
    public async Task Register_WithValidForm_ShouldRegisterAccount()
    {
        //arrange
        var vm = new RegisterVm()
        {
            Email = "phill@gmail.com",
            FirstName = "Phill",
            LastName = "Cash",
            Password = "Test123!",
            PasswordConfirmation = "Test123!",
        };

        var formContent = vm.ToFormContent();

        //act
        var response = await _client.PostAsync("/account/register", formContent);

        //assert
        var result = await _db.Users.AnyAsync();

        result.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public Task DisposeAsync() => _resetDb();

    public Task InitializeAsync() => Task.CompletedTask;
}
