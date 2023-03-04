using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public sealed class LastName
    {
        public string Value { get; }
        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidLastNameException($"This lastName is invalid: {value}");

            Value = value;
        }

        public static implicit operator LastName(string value) => new LastName(value);

        public static implicit operator string(LastName lastName) => lastName.Value;
    }
}


