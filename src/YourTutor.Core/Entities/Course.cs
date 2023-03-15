using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }

        public TutorId TutorId { get; set; }
    }
}


