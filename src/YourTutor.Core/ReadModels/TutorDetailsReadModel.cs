namespace YourTutor.Core.ReadModels;

public sealed record TutorDetailsReadModel(Guid TutorId, string FullName, string Email, string Description, string Country, string Language);


