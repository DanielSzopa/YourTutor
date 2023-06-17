using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class OfferIdTests
{
    [Fact]
    public void CreateInstance_WhenValueIsGuidEmpty_ShouldThrowInvalidOfferIdException()
    {
        //act
        Action result = () => { new OfferId(Guid.Empty); };

        //assert
        result.Should().Throw<InvalidOfferIdException>();
    }

    [Fact]
    public void CreateInstance_WhenValueIsValid_ShouldNotThrowInvalidOfferIdException()
    {
        //act
        Action result = () => { new OfferId(Guid.NewGuid()); };

        //assert
        result.Should().NotThrow<InvalidOfferIdException>();
    }
}


