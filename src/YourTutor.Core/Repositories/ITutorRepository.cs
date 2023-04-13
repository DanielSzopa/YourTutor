using YourTutor.Core.Entities;
using YourTutor.Core.ReadModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Repositories
{
    public interface ITutorRepository
    {
        Task<TutorDetailsReadModel> GetTutorDetailsByUserId(Guid userId);
        Task<Tutor> GetTutorById(Guid userId);

        Task<TutorDetailsForEditReadModel> GetTutorDetailsForEdit(UserId userId);
    }
}
