namespace YourTutor.Core.Exceptions;

internal sealed class InvalidPriceException : CustomException
{
    internal InvalidPriceException(int price) : base($"Invalid price, price: {price}")
    {

    }
}


