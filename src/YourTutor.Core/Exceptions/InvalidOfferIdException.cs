namespace YourTutor.Core.Exceptions;

internal sealed class InvalidOfferIdException : CustomException
{
    internal InvalidOfferIdException(Guid value) : base($"Invalid offerId, value: {value}")
    {
        
    }
}


