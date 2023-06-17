using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class HashPasswordTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;
    public HashPasswordTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void HashPassword_WhenValueIsEmptyOrNull_ShouldThrowInvalidPasswordException(string value)
    {
        try
        {
            //act
            var hashPassword = new HashPassword(value);
        }
        catch (InvalidPasswordException ex)
        {
            //assert
            Assert.True(true);
            return;
        }

        //assert
        Assert.True(false);
    }

    [Fact]
    public void HashPassword_WhenValueIsValid_ShouldNotThrowInvalidPasswordException()
    {
        try
        {
            //act
            var hashPassword = new HashPassword(_faker.Random.String2(10));
        }
        catch (InvalidPasswordException ex)
        {
            //assert
            Assert.True(false);
            return;
        }

        //assert
        Assert.True(true);
    }
}


