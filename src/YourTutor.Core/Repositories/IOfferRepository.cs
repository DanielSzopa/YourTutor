using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Repositories;

public interface IOfferRepository
{
    Task CreateOffer(Offer offer);

    Task RemoveOfferById(OfferId id);

    Task<OfferDetailsReadmodel> GetOfferDetails(OfferId id);

    Task<bool> CheckIfUserHasAccessToOffer(OfferId offerId, UserId userId);

    Task<SmallOfferPaginationReadModel> GetSmallOffers(bool isRemotely, bool isRemotelyFiltered, int priceFrom, int priceTo, int pageSize, int excludeRecords, string searchString);
}
