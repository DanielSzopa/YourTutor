using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Tutor
    {
        private readonly List<Experience> _experienceList = new();
        private readonly List<Course> _coursesList = new();

        public UserId UserId { get; private set; }

        public string Description { get; private set; }
        public string Country { get; private set; }
        public string Language { get; private set; }


        public IReadOnlyCollection<Experience> Experiences => _experienceList;
        public IReadOnlyCollection<Course> Courses => _coursesList;

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


