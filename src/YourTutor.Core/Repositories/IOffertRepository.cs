using YourTutor.Core.Entities;

namespace YourTutor.Core.Repositories;

public interface IOffertRepository
{
    Task CreateOffert(Offert offert);
}
