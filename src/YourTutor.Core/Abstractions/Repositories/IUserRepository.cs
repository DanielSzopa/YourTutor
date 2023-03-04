using YourTutor.Core.Entities;

namespace YourTutor.Core.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid userId);

        Task AddUser(User user);

        Task<bool> IsEmailAlreadyExists(string email);
    }
}
