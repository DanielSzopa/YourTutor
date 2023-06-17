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
    public void Price_WhenValueIsNotGreaterThanZero_ShouldThrowInvalidPriceException(int value)
    {
        try
        {
            //act
            var price = new Price(value);
        }
        catch (InvalidPriceException ex)
        {
            //assert
            Assert.True(true);
            return;
        }

        //assert
        Assert.True(false);
    }

    [Fact]
    public void Price_WhenValueIsValid_ShouldNotThrowInvalidPriceException()
    {
        try
        {
            //act
            var price = new Price(1);
        }
        catch (InvalidPriceException ex)
        {
            //assert
            Assert.True(false);
            return;
        }

        //assert
        Assert.True(true);
    }
}


