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

    public async Task<OffertDetailsReadModel> GetOffertDetails(OffertId id)
    {
        var offert = await _db
            .Offerts
            .Include(o => o.Tutor)
            .ThenInclude(t => t.User)
            .Where(o => o.Id == id)
            .Select(o => new OffertDetailsReadModel(o.Id, o.Description, o.Subject, o.Price, o.Location, o.IsRemotely, o.Tutor.User.FirstName + " " + o.Tutor.User.LastName,
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

    public async Task<SmallOffertPaginationReadModel> GetSmallOfferts(bool isRemotely, bool isRemotelyFiltered, int priceFrom, int priceTo, int pageSize, int excludeRecords, string searchString)
    {
        var query = _db.
            Offerts
            .Include(o => o.Tutor)
            .ThenInclude(t => t.User)
            .AsQueryable();

        searchString = searchString?.ToLower();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query
                .Where(o => o.Subject.ToLower().Contains(searchString)
                || o.Location.ToLower().Contains(searchString)
                || ((string)o.Tutor.User.Email).ToLower().Contains(searchString)
                || ((string)o.Tutor.User.FirstName).ToLower().Contains(searchString)
                || ((string)o.Tutor.User.LastName).ToLower().Contains(searchString));
        }

        if (isRemotelyFiltered)
            query = query
                .Where(o => o.IsRemotely == isRemotely);

        if (priceFrom > 0 && priceTo > 0)
        {
            query = query
               .Where(o => o.Price >= new Price(priceFrom) && o.Price <= new Price(priceTo));
        }

        var quantity = await query.CountAsync();

        query = query
            .Skip(excludeRecords)
            .Take(pageSize);

        var items = await query
            .Select(o => new SmallOffertsReadModel(o.Id, o.Subject, o.Price, o.Location, o.IsRemotely, o.Tutor.User.FirstName + " " + o.Tutor.User.LastName, o.Tutor.User.Email))
            .ToListAsync();

        return new SmallOffertPaginationReadModel(items, quantity);
    }
}


