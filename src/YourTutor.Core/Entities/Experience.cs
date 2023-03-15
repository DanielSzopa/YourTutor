using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Experience
    {
        public int ExperienceId { get; set; }
        public string Name { get; set; }

        public TutorId TutorId { get; set; }
    }
}


