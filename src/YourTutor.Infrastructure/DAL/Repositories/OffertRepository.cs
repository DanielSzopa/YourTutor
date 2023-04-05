using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

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

    public async Task<IReadOnlyCollection<SmallOffertsReadModel>> GetSmallOfferts(IQueryable<Offert> offertsQuery)
    {
        var offerts = await offertsQuery
            .Select(o => new SmallOffertsReadModel(o.Id, o.Subject, o.Price, o.Location, o.IsRemotely, o.Tutor.User.FirstName + " " + o.Tutor.User.LastName, o.Tutor.User.Email))
            .ToListAsync();

        return offerts;
    }

    public IQueryable<Offert> GetOffertsAsQueryable()
    {
        return _db.
            Offerts
            .Include(o => o.Tutor)
            .ThenInclude(t => t.User);
    }

    public async Task<int> CountOfferts(IQueryable<Offert> offerts)
        => await offerts.CountAsync();

    public async Task<OffertDetailsReadmodel> GetOffertDetails(OffertId id)
    {
        var offert = await _db
            .Offerts
            .Include(o => o.Tutor)
            .ThenInclude(t => t.User)
            .Where(o => o.Id == id)
            .Select(o => new OffertDetailsReadmodel(o.Id, o.Description, o.Subject, o.Price, o.Location, o.IsRemotely, o.Tutor.User.FirstName + " " + o.Tutor.User.LastName,
                o.Tutor.User.Email, o.Tutor.Country, o.Tutor.Language, o.Tutor.UserId))
            .FirstOrDefaultAsync();

        return offert;
    }

    public async Task<bool> CheckIfUserHasAccessToOffert(OffertId offertId, UserId userId)
    {
        var result = await _db
            .Offerts
            .Include(o => o.Tutor)
            .ThenInclude(t => t.User)
            .AnyAsync(o => o.Id == offertId && o.Tutor.User.Id == userId);

        return result;
    }

    public async Task RemoveOffertById(OffertId id)
    {
        var offert = await _db.Offerts
            .FirstAsync(o => o.Id == id);

        _db.Remove(offert);
    }
}


