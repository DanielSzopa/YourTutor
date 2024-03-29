﻿using YourTutor.Application.Abstractions.Security;
using YourTutor.Infrastructure.Security;

namespace YourTutor.Tests.Unit.Core.Services
{
    public sealed class HashServiceTests
    {
        private readonly IHashService _hashService;
        public HashServiceTests()
        {
            _hashService = new HashService();
        }

        [Fact]
        public void HashPassword_ShouldHashPassword()
        {
            //arrange
            var password = "Test123!!!";

            //act
            var hashPassword = _hashService.HashPassword(password);

            //assert
            hashPassword.Should().NotBe(password);
        }

        [Theory]
        [InlineData("Test123!!!", "$2a$08$us3FkZ0RlzAumB9bLCVQjuLI7SWky91.6AWH3FwCW3jedMA8ZVlo2")]
        public void VerifyPassword_WhenPasswordsMatches_ShouldReturnTrue(string password, string hashPassword)
        {
            //act
            var result = _hashService.VerifyPassword(password, hashPassword);

            //assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Test321!!!", "$2a$08$us3FkZ0RlzAumB9bLCVQjuLI7SWky91.6AWH3FwCW3jedMA8ZVlo2")]
        [InlineData("Test123!!!", "$2a$08$us3FkZ0RlzAumB9bLCVQjuLI7SWky91.6AWH3FwCW3jedMA8ZVlo3")]
        public void VerifyPassword_WhenPasswordsDoesNotMatch_ShouldReturnFalse(string password, string hashPassword)
        {
            //act
            var result = _hashService.VerifyPassword(password, hashPassword);

            //assert
            result.Should().BeFalse();
        }
    }
}


