namespace YourTutor.Application.ViewModels;

public sealed record OffertDetailsVm
    (Guid OffertId, string Description, string Subject, int Price,
    string Location, bool IsRemotely, string FullName, string Email,
    string Country, string SpeakingLanguage, Guid TutorId);

