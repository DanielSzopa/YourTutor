using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Entities;
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
                .Select(t => new TutorDetailsReadModel($"{t.User.FirstName.Value} {t.User.LastName.Value}", t.User.Email, t.Description, t.Country, t.Language))
                .FirstOrDefaultAsync();

            return details;
        }

        public async Task<Tutor> GetTutorById(Guid userId)
        {
            var tutor = await _dbContext
                .Tutor
                .FirstOrDefaultAsync(t => t.UserId == new UserId(userId));
            return tutor;
        }
    }
}


