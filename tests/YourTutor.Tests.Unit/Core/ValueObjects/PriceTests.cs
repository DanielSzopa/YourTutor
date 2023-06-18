using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class PriceTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;

    public PriceTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CreateInstance_WhenValueIsNotGreaterThanZero_ShouldThrowInvalidPriceException(int value)
    {
        //act
        Action result = () => { new Price(value); };

        //assert
        result.Should().Throw<InvalidPriceException>();
    }

    [Fact]
    public void CreateInstance_WhenValueIsValid_ShouldNotThrowInvalidPriceException()
    {
        //act
        Action result = () => { new Price(1); };

        //assert
        result.Should().NotThrow<InvalidPriceException>();
    }
}


