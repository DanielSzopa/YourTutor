using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Tutor
    {
        public UserId UserId { get; private set; }
        public string Description { get; private set; }
        public string Country { get; private set; }
        public string Language { get; private set; }

        public User User { get; private set; }

        internal Tutor(UserId userId, string description, string country, string language)
        {
            UserId = userId;
            Description = description;
            Country = country;
            Language = language;
        }

    }
}


