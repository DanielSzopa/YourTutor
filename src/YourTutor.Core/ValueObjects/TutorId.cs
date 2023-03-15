using YourTutor.Core.Exceptions;

namespace YourTutor.Core.ValueObjects
{
    public sealed class TutorId
    {
        public Guid Value { get; }

        public TutorId(Guid value)
        {
            if (value == Guid.Empty)
                throw new InvalidTutorIdException($"Invalid TutorId: {value}");

            Value = value;
        }

        public static implicit operator TutorId(Guid value) => new(value);

        public static implicit operator Guid(TutorId tutorId) => tutorId.Value;
    }
}


