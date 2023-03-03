namespace YourTutor.Core.Exceptions
{
    public sealed class InvalidPasswordException : CustomException
    {
        internal InvalidPasswordException(string message) : base(message) { }
    
    }
}


