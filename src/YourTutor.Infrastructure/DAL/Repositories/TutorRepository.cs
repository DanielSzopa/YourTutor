using Microsoft.EntityFrameworkCore;
using YourTutor.Core.ReadModels;
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

        public async Task<TutorDetailsReadModel> GetTutorDetailsByUserId(Guid userId)
        {
            var details = await _dbContext.Tutor
                .Include(t => t.User)
                .Where(t => t.UserId == new UserId(userId))
                .Select(t => new TutorDetailsReadModel()
                {
                    FirstName = t.User.FirstName,
                    LastName = t.User.LastName,
                    Email = t.User.Email,
                    Description = t.Description,
                    Country = t.Country,
                    Language = t.Language
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return details;
        }
    }
}


