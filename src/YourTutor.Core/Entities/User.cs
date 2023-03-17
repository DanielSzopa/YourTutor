using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class User
    {
        public UserId Id { get; private set; }
        public Email Email { get; private set; }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public HashPassword HashPassword { get; private set; }

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
            Tutor = new Tutor(Id, string.Empty, string.Empty, string.Empty);
    }
}


