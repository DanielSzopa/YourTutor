using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Repositories;

public interface IOffertRepository
{
    Task CreateOffert(Offert offert);

    Task RemoveOffertById(OffertId id);

    Task<OffertDetailsReadModel> GetOffertDetails(OffertId id);

    Task<bool> CheckIfUserHasAccessToOffert(OffertId offertId, UserId userId);

    Task<SmallOffertPaginationReadModel> GetSmallOfferts(bool isRemotely, bool isRemotelyFiltered, int priceFrom, int priceTo, int pageSize, int excludeRecords, string searchString);
}
