using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Entities;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Infrastructure.DAL.Repositories
{
    internal sealed class TutorRepository : ITutorRepository
    {
        private readonly YourTutorDbContext _dbContext;

        public TutorRepository(YourTutorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tutor> GetTutorDetailsByUserId(Guid userId)
        {
            var tutor = await _dbContext.Tutor
                .Include(t => t.User)
                .SingleAsync(u => u.UserId == new UserId(userId));

            return tutor;
        }
    }
}


