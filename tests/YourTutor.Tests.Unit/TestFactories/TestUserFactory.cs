using YourTutor.Core.Entities;

namespace YourTutor.Tests.Unit.TestFactories;

internal static class TestUserFactory
{
    private static Faker _faker = new Faker();

    internal static User User => new(Guid.NewGuid(), _faker.Person.Email, _faker.Person.FirstName,
        _faker.Person.LastName, _faker.Random.String2(10));
}


