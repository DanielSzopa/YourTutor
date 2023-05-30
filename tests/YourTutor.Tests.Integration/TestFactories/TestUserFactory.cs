using YourTutor.Application.Abstractions.Security;
using YourTutor.Core.Entities;

namespace YourTutor.Tests.Integration.TestFactories;

public static class TestUserFactory
{
    private static readonly Faker _faker = new Faker();
    private static readonly string _password = "notHashedPass!1";

    public static User User
        => new(Guid.NewGuid(), _faker.Person.Email.ToLower(), _faker.Person.FirstName, _faker.Person.LastName, _password);

    public static TestUser GetTestUserWithHashing(IHashService hashService)
    {
        var hashedPassword = hashService.HashPassword(_password);
        var user = new User (Guid.NewGuid(), _faker.Person.Email.ToLower(), _faker.Person.FirstName, _faker.Person.LastName, hashedPassword);

        return new TestUser(user, _password);
    }
}

public record TestUser(User UserWithHashedPassword, string OrgiginalPassword);
