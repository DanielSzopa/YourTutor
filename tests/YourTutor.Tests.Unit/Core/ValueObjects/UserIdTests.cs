using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class UserIdTests
{
    [Fact]
    public void UserId_WhenValueIsGuidEmpty_ShouldThrowInvalidUserIdException()
    {
        //act
        Action result = () => { new UserId(Guid.Empty); };

        //assert
        result.Should().Throw<InvalidUserIdException>();
    }

    [Fact]
    public void OfferId_WhenValueIsValid_ShouldNotThrowInvalidUserIdException()
    {
        //act
        Action result = () => { new UserId(Guid.NewGuid()); };

        //assert
        result.Should().NotThrow<InvalidUserIdException>();
    }
}


