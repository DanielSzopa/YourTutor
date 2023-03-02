using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Core.Entities
{
    public sealed class User
    {
        public Email Email { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public Password Password { get; }
        public HashPassword HashPassword { get; private set; }
        public User(Email email, FirstName firstName, LastName lastName, Password password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public bool Register(Email email, FirstName firstName, LastName lastName, Password password, string confirmedPassword)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(confirmedPassword)
                    || password.Value.ToLower() != confirmedPassword.ToLower())
                {
                    throw new InvalidPasswordException($"Passwords does not match: password {password.Value}, confirmedPassword: {confirmedPassword}");
                }

                var user = new User(email, firstName, lastName, password);
                SetHashPassword(user.Password);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void SetHashPassword(string password) => HashPassword = password;

    }
}


