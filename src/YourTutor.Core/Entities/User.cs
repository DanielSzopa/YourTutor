using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class User
    {
        public UserId Id { get; }
        public Email Email { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public HashPassword HashPassword { get; }

        public Tutor Tutor { get; private set; }

        public User(UserId id, Email email, FirstName firstName, LastName lastName, HashPassword hashPassword)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            HashPassword = hashPassword;
        }

        public void CreateTutor() => 
            Tutor = new Tutor(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, Id);
    }
}


