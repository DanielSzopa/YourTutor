using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Entities;
using YourTutor.Core.Repositories;

namespace YourTutor.Infrastructure.DAL.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly YourTutorDbContext _dbContext;

        public UserRepository(YourTutorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.AddAsync<User>(user);
            await _dbContext.SaveChangesAsync();
        }

        public Task<User> GetUserByEmailAsync(string email) => _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        public Task<bool> IsEmailAlreadyExistsAsync(string email) => 
             _dbContext.Users.AnyAsync(u => u.Email == email);
    }
}


