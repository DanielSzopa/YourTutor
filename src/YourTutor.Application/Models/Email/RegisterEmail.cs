namespace YourTutor.Application.Models.EmailBase
{
    public class RegisterEmail : EmailBase
    {
        public RegisterEmail(string to, string from) : base("Information about account registration", "Your account has been registered", "Registration", to, from)
        {

        }
    }
}


