using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Tutor
    {
        private readonly List<Experience> _experienceList = new();
        private readonly List<Course> _coursesList = new();

        public TutorId TutorId { get; private set; }

        public string Description { get; private set; }
        public string Country { get; private set; }
        public string Language { get; private set; }


        public IReadOnlyCollection<Experience> Experiences => _experienceList;
        public IReadOnlyCollection<Course> Courses => _coursesList;


        public UserId UserId { get; private set; }
        public User User { get; private set; }

        internal Tutor(TutorId tutorId, string description, string country, string language, UserId userId)
        {
            TutorId = tutorId;
            Description = description;
            Country = country;
            Language = language;
            UserId = userId;
        }

    }
}


