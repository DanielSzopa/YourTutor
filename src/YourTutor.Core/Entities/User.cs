using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public Password Password { get; private set; }
        public HashPassword HashPassword { get; private set; }
        public User(Guid id, Email email, FirstName firstName, LastName lastName, Password password)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public void Register(string confirmedPassword)
        {
            if (string.IsNullOrWhiteSpace(confirmedPassword)
                || Password.Value != confirmedPassword)
            {
                throw new InvalidPasswordException($"Passwords does not match: password {Password}, confirmedPassword: {confirmedPassword}");
            }

            SetHashPassword(Password);
        }

        private void SetHashPassword(string password)
        {
            //Logic for HashingPassword
            HashPassword = password;
        }

        private void UnHashPassword(string hashPassoword)
        {
            //Logic for unHashPassword
            Password = hashPassoword;
        }

    }
}


