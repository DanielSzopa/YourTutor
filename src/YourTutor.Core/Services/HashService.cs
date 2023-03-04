namespace YourTutor.Core.Services
{
    internal static class HashService
    {
        internal static string HashPassword(string password) 
            => BCrypt.Net.BCrypt.HashPassword(password,GetSalt());

        internal static bool VerifyPassword(string password, string hashPassword) 
            => BCrypt.Net.BCrypt.Verify(password,hashPassword);

        private static string GetSalt() 
            => BCrypt.Net.BCrypt.GenerateSalt(8);
    }
}


