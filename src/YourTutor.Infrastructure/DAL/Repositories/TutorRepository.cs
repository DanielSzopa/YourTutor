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

        public async Task<TutorDetailsReadModel> GetTutorDetailsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var details = await _dbContext.Tutor
                .Include(t => t.User)
                .Where(t => t.UserId == new UserId(userId))
                .Select(t => new TutorDetailsReadModel(t.UserId, $"{t.User.FirstName.Value} {t.User.LastName.Value}", t.User.Email, t.Description, t.Country, t.Language))
                .FirstOrDefaultAsync(cancellationToken);

            return details;
        }

        public async Task<Tutor> GetTutorById(Guid userId, CancellationToken cancellationToken)
        {
            var tutor = await _dbContext
                .Tutor
                .FirstOrDefaultAsync(t => t.UserId == new UserId(userId), cancellationToken);
            return tutor;
        }

        public async Task<TutorDetailsForEditReadModel> GetTutorDetailsForEdit(UserId userId, CancellationToken cancellationToken)
        {
            var details = await _dbContext
                .Tutor
                .Where(t => t.UserId == userId)
                .Select(t => new TutorDetailsForEditReadModel(t.Description, t.Country, t.Language))
                .FirstOrDefaultAsync(cancellationToken);

            return details;
        }
    }
}


