namespace YourTutor.Core.ReadModels;

public sealed record SmallOffertsReadModel(Guid OffertId, string Subject, int Price, string Location, bool IsRemotely, string FullName, string Email);


