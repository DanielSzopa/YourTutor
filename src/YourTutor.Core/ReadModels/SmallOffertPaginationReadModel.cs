namespace YourTutor.Core.ReadModels;

public sealed record SmallOffertPaginationReadModel(IReadOnlyCollection<SmallOffertsReadModel> Offerts, int Count);


