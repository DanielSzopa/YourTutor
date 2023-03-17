using YourTutor.Core.ReadModels;

namespace YourTutor.Core.Repositories
{
    public interface ITutorRepository
    {
        Task<TutorDetailsReadModel> GetTutorDetailsByUserId(Guid userId);
    }
}
