namespace YourTutor.Core.ReadModels;

public sealed record SmallOffersReadModel(Guid OfferId, string Subject, string Description, int Price, string Location, bool IsRemotely, string FullName, string Email);


