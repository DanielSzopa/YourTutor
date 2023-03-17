namespace YourTutor.Application.Dtos.Tutor
{
    public sealed record TutorDto(string FullName, string Email, string Description, string Country, string Language, List<ExperienceDto> experiences, List<CourseDto> courses);
}


