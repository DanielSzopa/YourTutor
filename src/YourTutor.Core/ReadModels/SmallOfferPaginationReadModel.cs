namespace YourTutor.Core.ReadModels;

public sealed record SmallOfferPaginationReadModel(IReadOnlyCollection<SmallOffersReadModel> Offers, int Count);


