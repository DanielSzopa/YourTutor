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
        public Password Password { get; }
        public HashPassword HashPassword { get; private set; }
        public User(Guid id, Email email, FirstName firstName, LastName lastName, Password password)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public bool Register(Guid id, Email email, FirstName firstName, LastName lastName, Password password, string confirmedPassword)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(confirmedPassword)
                    || password.Value.ToLower() != confirmedPassword.ToLower())
                {
                    throw new InvalidPasswordException($"Passwords does not match: password {password.Value}, confirmedPassword: {confirmedPassword}");
                }

                var user = new User(id, email, firstName, lastName, password);
                SetHashPassword(user.Password);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SetHashPassword(string password) => HashPassword = password;

    }
}


