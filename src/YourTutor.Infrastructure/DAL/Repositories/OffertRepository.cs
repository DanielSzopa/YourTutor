using YourTutor.Core.Entities;
using YourTutor.Core.Repositories;

namespace YourTutor.Infrastructure.DAL.Repositories;

internal class OffertRepository : IOffertRepository
{
    private readonly YourTutorDbContext _db;

    public OffertRepository(YourTutorDbContext yourTutorDbContext)
    {
        _db = yourTutorDbContext;
    }

    public async Task CreateOffert(Offert offert)
    {
        await _db
            .AddAsync(offert);
    }
}


