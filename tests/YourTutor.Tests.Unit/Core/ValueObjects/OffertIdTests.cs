using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.ValueObjects;

public class OfferIdTests
{
    [Fact]
    public void OfferId_WhenValueIsGuidEmpty_ShouldThrowInvalidOfferIdException()
    {
        try
        {
            //act
            var offerId = new OfferId(Guid.Empty);
        }
        catch (InvalidOfferIdException ex)
        {
            //assert
            Assert.True(true);
            return;
        }

        //assert
        Assert.True(false);
    }

    [Fact]
    public void OfferId_WhenValueIsValid_ShouldNotThrowInvalidOfferIdException()
    {
        try
        {
            //act
            var offerId = new OfferId(Guid.NewGuid());
        }
        catch (InvalidOfferIdException ex)
        {
            //assert
            Assert.True(false);
            return;
        }

        //assert
        Assert.True(true);
    }
}


