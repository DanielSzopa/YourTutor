using YourTutor.Application.Abstractions.Security;

namespace YourTutor.Infrastructure.Security
{
    public class HashService : IHashService
    {
        public string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, GetSalt());

        public bool VerifyPassword(string password, string hashPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashPassword);

        private string GetSalt()
            => BCrypt.Net.BCrypt.GenerateSalt(8);
    }
}


