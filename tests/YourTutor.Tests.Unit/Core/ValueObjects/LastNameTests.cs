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
    public void CreateInstance_WhenValueIsEmptyOrNull_ShouldThrowInvalidLastNameException(string value)
    {
        //act
        Action result = () => { new LastName(value); };

        //assert
        result.Should().Throw<InvalidLastNameException>();
    }

    [Fact]
    public void CreateInstance_WhenValueIsValid_ShouldNotThrowInvalidLastNameException()
    {
        //act
        Action result = () => { new LastName(_faker.Person.LastName); };

        //assert
        result.Should().NotThrow<InvalidLastNameException>();
    }
}


