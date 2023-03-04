using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public sealed class Password
    {
        public string Value { get; }

        public Password(string password)
        {
            if(string.IsNullOrWhiteSpace(password)
                || password.Length < 8
                || password.Length > 20)
            {
                throw new InvalidPasswordException("Password should be between 8 and 20 characters");
            }

            Value = password;
        }


        public static implicit operator Password(string value) => new Password(value);

        public static implicit operator string(Password password) => password.Value;
    }
}


