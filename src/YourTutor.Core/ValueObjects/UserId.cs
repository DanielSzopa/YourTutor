using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public class UserId
    {
        public Guid Value { get; }

        public UserId(Guid value)
        {
            if (value == Guid.Empty)
                throw new InvalidUserIdException($"Invalid UserId: {value}");

            Value = value;
        }

        public static implicit operator UserId(Guid value) => new UserId(value);

        public static implicit operator Guid(UserId userId) => userId.Value;
    }
}


