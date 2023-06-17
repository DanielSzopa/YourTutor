using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class EmailTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;
    public EmailTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void Email_WhenValueIsEmptyOrNull_ShouldThrowInvalidEmailException(string value)
    {
        try
        {
            //act
            var email = new Email(value);
        }
        catch (InvalidEmailException ex)
        {
            //assert
            Assert.True(true);
            return;
        }

        //assert
        Assert.True(false);
    }

    [Fact]
    public void Email_WhenValueIsValid_ShouldNotThrowInvalidEmailException()
    {
        try
        {
            //act
            var email = new Email(_faker.Person.Email);
        }
        catch (InvalidEmailException ex)
        {
            //assert
            Assert.True(false);
            return;
        }

        //assert
        Assert.True(true);
    }
}


