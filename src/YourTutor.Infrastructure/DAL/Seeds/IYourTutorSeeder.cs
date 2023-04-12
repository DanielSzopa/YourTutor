using YourTutor.Core.Entities;

namespace YourTutor.Infrastructure.DAL.Seeds
{
    public interface IYourTutorSeeder
    {
        IReadOnlyCollection<User> GetSeedData();
    }
}