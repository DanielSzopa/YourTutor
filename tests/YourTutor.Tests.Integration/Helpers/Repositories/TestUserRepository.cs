using YourTutor.Core.Entities;
using YourTutor.Infrastructure.DAL;

namespace YourTutor.Tests.Integration.Helpers.Repositories;

internal class TestUserRepository
{
    private readonly YourTutorDbContext _db;

    internal TestUserRepository(YourTutorDbContext yourTutorDbContext)
    {
        _db = yourTutorDbContext;
    }

    public async Task AddUserAsync(User user)
    {
        await _db.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> GetFirstUserAsync() => 
        await _db.Users
        .AsNoTracking()
            .Include(u => u.Tutor)
            .FirstOrDefaultAsync();

    public async Task<bool> AnyAsync() => 
        await _db.Users.AnyAsync();
}
