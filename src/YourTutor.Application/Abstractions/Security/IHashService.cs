﻿namespace YourTutor.Application.Abstractions.Security
{
    public interface IHashService
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string password, string hashPassword);

    }
}
