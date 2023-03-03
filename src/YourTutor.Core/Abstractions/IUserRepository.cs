using YourTutor.Core.Entities;

namespace YourTutor.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid userId);

        Task AddUser(User user);        
    }
}
