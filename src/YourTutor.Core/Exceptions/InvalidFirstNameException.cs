namespace YourTutor.Core.Exceptions
{
    public sealed class InvalidFirstNameException : CustomException
    {
        internal InvalidFirstNameException(string message) : base(message) { }
    }
}


