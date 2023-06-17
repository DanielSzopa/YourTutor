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
    public void CreateInstance_WhenValueIsEmptyOrNull_ShouldThrowInvalidFirstNameException(string value)
    {
        //act
        Action result = () => { new FirstName(value); };

        //assert
        result.Should().Throw<InvalidFirstNameException>();
    }

    [Fact]
    public void CreateInstance_WhenValueIsValid_ShouldNotThrowInvalidFirstNameException()
    {
        //act
        Action result = () => { new FirstName(_faker.Person.FirstName); };

        //assert
        result.Should().NotThrow<InvalidFirstNameException>();
    }
}


