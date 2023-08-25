using System.Threading;
using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Repositories;

public interface IOfferRepository
{
    Task CreateOffer(Offer offer, CancellationToken cancellationToken);

    Task RemoveOfferById(OfferId id, CancellationToken cancellationToken);

    Task<OfferDetailsReadmodel> GetOfferDetails(OfferId id, CancellationToken cancellationToken);

    Task<bool> CheckIfUserHasAccessToOffer(OfferId offerId, UserId userId, CancellationToken cancellationToken);

    Task<SmallOfferPaginationReadModel> GetSmallOffers(bool isRemotely, bool isRemotelyFiltered, int priceFrom, int priceTo, int pageSize, int excludeRecords, string searchString, CancellationToken cancellationToken);
}
