namespace YourTutor.Core.Exceptions
{
    public sealed class EmailAlreadyExistsException : CustomException
    {
        public EmailAlreadyExistsException(string message) : base(message)
        {

        }
    }
}


