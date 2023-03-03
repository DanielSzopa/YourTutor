using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Entities;

namespace YourTutor.Infrastructure.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly YourTutorDbContext _dbContext;

        public UserRepository(YourTutorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user)
        {
            await _dbContext.AddAsync<User>(user);
            await _dbContext.SaveChangesAsync();
        }

        public Task<User> GetUserById(Guid userId) => _dbContext.Users
            .SingleOrDefaultAsync(u => u.Id == userId);
    }
}


