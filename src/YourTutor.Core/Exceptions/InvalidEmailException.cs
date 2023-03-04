namespace YourTutor.Core.Exceptions
{
    internal sealed class InvalidEmailException : CustomException
    {
        internal InvalidEmailException(string message) : base(message)
        {
            
        }
    }
}


