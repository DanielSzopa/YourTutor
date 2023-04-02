using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.ReadModels;

public sealed record SmallOffertsReadModel(OffertId OffertId, string Subject, Price Price, string Location, bool IsRemotely, string FullName, Email Email);


