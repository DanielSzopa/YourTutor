using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public sealed record HashPassword
    {
        public string Value { get; }

        public HashPassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidPasswordException("Invalid password for hashing");

            Value = value;
        }


        public static implicit operator HashPassword(string value) => new(value);

        public static implicit operator string(HashPassword password) => password.Value;
    }
}


