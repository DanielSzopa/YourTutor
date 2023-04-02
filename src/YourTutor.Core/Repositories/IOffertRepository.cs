using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;

namespace YourTutor.Core.Repositories;

public interface IOffertRepository
{
    Task CreateOffert(Offert offert);

    Task<IReadOnlyCollection<SmallOffertsReadModel>> GetSmallOfferts();
}
