﻿using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
    public AccountControllerTests(YourTutorApp app)
    {
        _client = app.Client;
        _resetDb = app.ResetDbAsync;
        _db = app.YourTutorDbContext;
    }

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
        var result = await _db.Users.AnyAsync();

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Register_WithValidVm_Should_Return302Found_SetLocationHeader()
    {
        //arrange
        var formContent = _validRegisterVm.ToFormContent();

        //act
        var response = await _client.PostAsync(_registerEndpoint, formContent);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Found);
        response.Headers.Location?.OriginalString.Should().Be("/");
    }

    [Fact]
    public async Task Register_WithInvalidVm_Should_Return200Ok_WithoutLocationHeader()
    {
        //arrange
        var formContent = _invalidRegisterVm.ToFormContent();

        //act
        var response = await _client.PostAsync(_registerEndpoint, formContent);

        //assert
        using var scope = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Location.Should().BeNull();
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

    public Task DisposeAsync() => _resetDb();

    public Task InitializeAsync() => Task.CompletedTask;
}