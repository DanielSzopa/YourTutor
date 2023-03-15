using YourTutor.Core.Entities;

namespace YourTutor.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);

        Task<bool> IsEmailAlreadyExistsAsync(string email);
    }
}
