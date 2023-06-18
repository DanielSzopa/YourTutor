using YourTutor.Core.Entities;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.Entities;

public class OfferTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;

    public OfferTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Fact]
    public void Create_WithValidProperties_ShouldNotThrowAnyException()
    {
        //act
        Action result = () =>
        {
            new Offer(Guid.NewGuid(), _faker.Random.String2(10),
       _faker.Random.String2(10), 1,
       true, _faker.Random.String2(10), Guid.NewGuid());
        };

        //assert
        result.Should().NotThrow<Exception>();
    }

    [Fact]
    public void Create_WithInValidProperty_ShouldThrowException()
    {
        //act
        Action result = () =>
        {
            new Offer(Guid.Empty, _faker.Random.String2(10),
      _faker.Random.String2(10), 1,
      true, _faker.Random.String2(10), Guid.NewGuid());
        };

        //assert
        result.Should().Throw<Exception>();
    }
}


