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
    public void CreateInstance_WhenValueIsEmptyOrNull_ShouldThrowInvalidEmailException(string value)
    {
        //act
        Action result = () => { var email = new Email(value); };

        //assert
        result.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void CreateInstance_WhenValueIsValid_ShouldNotThrowInvalidEmailException()
    {
        //act
        Action result = () => { var email = new Email(_faker.Person.Email); };

        //assert
        result.Should().NotThrow<InvalidEmailException>();
    }
}


