using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public sealed record Password
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

        public void CheckIfPasswordsMatch(string confirmedPassword)
        {
            if (string.IsNullOrWhiteSpace(confirmedPassword)
                || Value != confirmedPassword)
            {
                throw new InvalidPasswordException($"Passwords does not match");
            }
        }


        public static implicit operator Password(string value) => new(value);

        public static implicit operator string(Password password) => password.Value;
    }
}


