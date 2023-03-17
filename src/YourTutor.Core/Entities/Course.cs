using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Course
    {
        public int CourseId { get; private set; }
        public string Name { get; private set; }

        public TutorId TutorId { get; private set; }
    }
}


