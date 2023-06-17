using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class LastNameTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;
    public LastNameTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void LastName_WhenValueIsEmptyOrNull_ShouldThrowInvalidLastNameException(string value)
    {
        try
        {
            //act
            var lastName = new LastName(value);
        }
        catch (InvalidLastNameException ex)
        {
            //assert
            Assert.True(true);
            return;
        }

        //assert
        Assert.True(false);
    }

    [Fact]
    public void LastName_WhenValueIsValid_ShouldNotThrowInvalidLastNameException()
    {
        try
        {
            //act
            var lastName = new LastName(_faker.Person.LastName);
        }
        catch (InvalidLastNameException ex)
        {
            //assert
            Assert.True(false);
            return;
        }

        //assert
        Assert.True(true);
    }
}


