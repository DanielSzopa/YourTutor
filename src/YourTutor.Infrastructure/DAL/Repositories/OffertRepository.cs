using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
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

    public async Task<IReadOnlyCollection<SmallOffertsReadModel>> GetSmallOfferts()
    {
        var offerts = await _db
            .Offerts
            .Include(o => o.Tutor)
            .ThenInclude(t => t.User)
            .Select(o => new SmallOffertsReadModel(o.Id, o.Subject, o.Price, o.Location, o.IsRemotely, $"{o.Tutor.User.FirstName.Value} {o.Tutor.User.LastName.Value}", o.Tutor.User.Email))
            .ToListAsync();

        return offerts;
    }
}


