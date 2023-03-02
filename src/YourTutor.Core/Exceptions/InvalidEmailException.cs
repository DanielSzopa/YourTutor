using System.Net;

namespace YourTutor.Core.Exceptions
{
    public class InvalidEmailException : CustomException
    {
        public InvalidEmailException(string message) : base(message)
        {
            
        }
    }
}


