namespace YourTutor.Core.Exceptions
{
    public sealed class InvalidEmailException : CustomException
    {
        internal InvalidEmailException(string message) : base(message)
        {
            
        }
    }
}


