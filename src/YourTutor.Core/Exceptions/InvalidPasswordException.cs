namespace YourTutor.Core.Exceptions
{
    internal sealed class InvalidPasswordException : CustomException
    {
        internal InvalidPasswordException(string message) : base(message) { }
    
    }
}


