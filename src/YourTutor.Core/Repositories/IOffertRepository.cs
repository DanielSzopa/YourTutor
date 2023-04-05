using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Repositories;

public interface IOffertRepository
{
    Task CreateOffert(Offert offert);

    Task<int> CountOfferts(IQueryable<Offert> offerts);

    Task<IReadOnlyCollection<SmallOffertsReadModel>> GetSmallOfferts(IQueryable<Offert> offertsQuery);

    IQueryable<Offert> GetOffertsAsQueryable();
    Task<OffertDetailsReadmodel> GetOffertDetails(OffertId id);

    Task<bool> CheckIfUserHasAccessToOffert(OffertId offertId, UserId userId);
}
