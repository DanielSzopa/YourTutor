using YourTutor.Core.Entities;

namespace YourTutor.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid userId);
        Task<User> GetUserByEmail(string email);

        Task AddUser(User user);

        Task<bool> IsEmailAlreadyExists(string email);
    }
}
