using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Repositories
{
    public interface ITutorRepository
    {
        Task<TutorDetailsReadModel> GetTutorDetailsByUserId(Guid userId, CancellationToken cancellationToken);
        Task<Tutor> GetTutorById(Guid userId, CancellationToken cancellationToken);

        Task<TutorDetailsForEditReadModel> GetTutorDetailsForEdit(UserId userId, CancellationToken cancellationToken);
    }
}
