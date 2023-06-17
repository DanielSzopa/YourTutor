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
    public void CreateInstance_WhenValueIsEmptyOrNull_ShouldThrowInvalidPasswordException(string value)
    {
        //act
        Action result = () => { new HashPassword(value); };

        //assert
        result.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void CreateInstance_WhenValueIsValid_ShouldNotThrowInvalidPasswordException()
    {
        //act
        Action result = () => { new HashPassword(_faker.Random.String2(10)); };

        //assert
        result.Should().NotThrow<InvalidPasswordException>();
    }
}


