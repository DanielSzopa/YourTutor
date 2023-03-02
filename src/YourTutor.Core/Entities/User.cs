using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class User
    {
        public Email Email { get; }
        public User(Email email)
        {
            Email = email;
        }
    }
}


