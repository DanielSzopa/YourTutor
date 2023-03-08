using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Entities;

namespace YourTutor.Infrastructure.DAL.Repositories
{
    internal sealed class UserRepository : IUserRepository
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

        public async Task<User> GetUserByEmail(string email) => await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        public Task<User> GetUserById(Guid userId) => _dbContext.Users
            .SingleOrDefaultAsync(u => u.Id == userId);

        public async Task<bool> IsEmailAlreadyExists(string email) => 
            await _dbContext.Users.AnyAsync(u => u.Email == email);
    }
}


