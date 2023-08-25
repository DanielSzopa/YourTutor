using YourTutor.Core.Entities;

namespace YourTutor.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task AddUserAsync(User user, CancellationToken cancellationToken);

        Task<bool> IsEmailAlreadyExistsAsync(string email, CancellationToken cancellationToken);
    }
}
