using YourTutor.Core.Entities;

namespace YourTutor.Core.Repositories
{
    public interface ITutorRepository
    {
        Task<Tutor> GetTutorDetailsByUserId(Guid userId);
    }
}
