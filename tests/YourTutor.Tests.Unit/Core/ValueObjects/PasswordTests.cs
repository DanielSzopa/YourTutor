using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;
using YourTutor.Tests.Unit.Fixtures;

namespace YourTutor.Tests.Unit.Core.ValueObjects
{
    public class PasswordTests : IClassFixture<FakerFixture>
    {
        private readonly Faker _faker;

        public PasswordTests(FakerFixture fakerFixture)
        {
            _faker = fakerFixture.Faker;
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Password_WhenValueIsEmptyOrNull_ShouldThrowInvalidPasswordException(string value)
        {
            //act
            Action result = () => { new Password(value); };

            //assert
            result.Should().Throw<InvalidPasswordException>();
        }

        [Theory]
        [InlineData(21)]
        [InlineData(7)]
        public void Password_WhenPasswordsLengthIsInvalid_ShouldThrowInvalidPasswordException(int length)
        {
            //act
            Action result = () => { new Password(_faker.Random.String2(length)); };

            //assert
            result.Should().Throw<InvalidPasswordException>();
        }

        [Theory]
        [InlineData(8)]
        [InlineData(15)]
        [InlineData(20)]
        public void Password_WhenPasswordsLengthIsValid_ShouldNotThrowInvalidPasswordException(int length)
        {
            //act
            Action result = () => { new Password(_faker.Random.String2(length)); };

            //assert
            result.Should().NotThrow<InvalidPasswordException>();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void CheckIfPasswordsMatch_WhenConfirmedPasswordIsNullOrEmpty_ShouldThrowInvalidPasswordException(string confirmedPassword)
        {
            //arrange
            var password = new Password(_faker.Random.String2(8));

            //act
            Action result = () => { password.CheckIfPasswordsMatch(confirmedPassword); };

            //assert
            result.Should().Throw<InvalidPasswordException>();
        }

        [Fact]
        public void CheckIfPasswordsMatch_WhenConfirmedDoesNotMatch_ShouldThrowInvalidPasswordException()
        {
            //arrange
            var confirmedPassword = _faker.Random.String2(10);
            var password = new Password(_faker.Random.String2(8));

            //act
            Action result = () => { password.CheckIfPasswordsMatch(confirmedPassword); };

            //assert
            result.Should().Throw<InvalidPasswordException>();
        }

        [Fact]
        public void CheckIfPasswordsMatch_WhenConfirmedMatched_ShouldNotThrowInvalidPasswordException()
        {
            //arrange
            var password = new Password(_faker.Random.String2(8));

            //act
            Action result = () => { password.CheckIfPasswordsMatch(password.Value); };

            //assert
            result.Should().NotThrow<InvalidPasswordException>();
        }
    }
}


