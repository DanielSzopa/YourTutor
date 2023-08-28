using Microsoft.EntityFrameworkCore;
using System.Threading;
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

        public async Task AddUserAsync(User user, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken) => _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        public Task<bool> IsEmailAlreadyExistsAsync(string email, CancellationToken cancellationToken) => 
             _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}


