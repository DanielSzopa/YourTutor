using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;

namespace YourTutor.Core.Repositories;

public interface IOffertRepository
{
    Task CreateOffert(Offert offert);

    Task<int> CountOfferts(IQueryable<Offert> offerts);

    Task<IReadOnlyCollection<SmallOffertsReadModel>> GetSmallOfferts(IQueryable<Offert> offertsQuery);

    IQueryable<Offert> GetOffertsAsQueryable();
}
