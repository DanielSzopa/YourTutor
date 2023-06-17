using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class UserIdTests
{
    [Fact]
    public void UserId_WhenValueIsGuidEmpty_ShouldThrowInvalidUserIdException()
    {
        try
        {
            //act
            var userId = new UserId(Guid.Empty);
        }
        catch (InvalidUserIdException ex)
        {
            //assert
            Assert.True(true);
            return;
        }

        //assert
        Assert.True(false);
    }

    [Fact]
    public void OfferId_WhenValueIsValid_ShouldNotThrowInvalidUserIdException()
    {
        try
        {
            //act
            var userId = new UserId(Guid.NewGuid());
        }
        catch (InvalidUserIdException ex)
        {
            //assert
            Assert.True(false);
            return;
        }

        //assert
        Assert.True(true);
    }
}


