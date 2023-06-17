using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class FirstNameTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;
    public FirstNameTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public async Task FirstName_WhenValueIsEmptyOrNull_ShouldThrowInvalidFirstNameException(string value)
    {
        try
        {
            //act
            var firstName = new FirstName(value);
        }
        catch (InvalidFirstNameException ex)
        {
            //assert
            Assert.True(true);
            return;
        }

        //assert
        Assert.True(false);
    }

    [Fact]
    public async Task FirstName_WhenValueIsValid_ShouldNotThrowInvalidFirstNameException()
    {
        try
        {
            //act
            var firstName = new Email(_faker.Person.FirstName);
        }
        catch (InvalidFirstNameException ex)
        {
            //assert
            Assert.True(false);
            return;
        }

        //assert
        Assert.True(true);
    }
}


