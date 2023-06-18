using YourTutor.Core.Entities;

namespace YourTutor.Tests.Unit.TestFactories;

internal static class TestOfferFactory
{
    private static Faker _faker = new Faker();

    internal static Offer Offer => new(Guid.NewGuid(), _faker.Random.String2(10),
        _faker.Random.String2(10), 1,
        true, _faker.Random.String2(10), Guid.NewGuid());
}


