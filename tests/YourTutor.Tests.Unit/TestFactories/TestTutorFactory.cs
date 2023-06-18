using YourTutor.Core.Entities;

namespace YourTutor.Tests.Unit.TestFactories;

internal static class TestTutorFactory
{
    private static Faker _faker = new Faker();

    internal static Tutor Tutor => 
        new(Guid.NewGuid(), _faker.Random.String2(10),
            _faker.Random.String2(10), _faker.Random.String2(10));
}


