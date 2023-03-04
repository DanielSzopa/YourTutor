using YourTutor.Core.Exceptions;
using YourTutor.Core.Services;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class User
    {
        public UserId Id { get; }
        public Email Email { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public Password Password { get; private set; }
        public HashPassword HashPassword { get; private set; }

        public User(UserId id, Email email, FirstName firstName, LastName lastName, Password password)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public User(UserId id, Email email, FirstName firstName, LastName lastName, HashPassword hashPassword)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            HashPassword = hashPassword;
        }

        public void Register(string confirmedPassword)
        {
            if (string.IsNullOrWhiteSpace(confirmedPassword)
                || Password.Value != confirmedPassword)
            {
                throw new InvalidPasswordException($"Passwords does not match");
            }

            SetHashPassword();
        }

        private void SetHashPassword() => HashPassword = HashService.HashPassword(Password);
    }
}


