namespace YourTutor.Core.Exceptions;

internal sealed class InvalidOffertIdException : CustomException
{
    internal InvalidOffertIdException(Guid value) : base($"Invalid offertId, value: {value}")
    {
        
    }
}


