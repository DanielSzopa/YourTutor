using FluentAssertions.Execution;
using YourTutor.Core.Entities;
using YourTutor.Tests.Unit.Fixtures;
using YourTutor.Tests.Unit.TestFactories;

namespace YourTutor.Tests.Unit.Core.Entities;

public class UserTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;

    public UserTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Fact]
    public void Create_WithValidProperties_ShouldNotThrowAnyException()
    {
        //act
        Action result = () =>
        {
            new User(Guid.NewGuid(), _faker.Person.Email, _faker.Person.FirstName,
        _faker.Person.LastName, _faker.Random.String2(10));
        };

        //assert
        result.Should().NotThrow<Exception>();
    }

    [Fact]
    public void Create_WithInValidProperty_ShouldThrowException()
    {
        //act
        Action result = () =>
        {
            new User(Guid.Empty, _faker.Person.Email, _faker.Person.FirstName,
        _faker.Person.LastName, _faker.Random.String2(10));
        };

        //assert
        result.Should().Throw<Exception>();
    }

    [Fact]
    public void CreateTutor_ShouldCreateTutor()
    {
        //arrange
        var user = TestUserFactory.User;

        //act
        user.CreateTutor();

        //assert
        using var scope = new AssertionScope();
        user.Tutor.Should().NotBeNull();
        user.Tutor.UserId.Should().Be(user.Id);
        user.Tutor.Description.Should().BeEmpty();
        user.Tutor.Country.Should().BeEmpty();
        user.Tutor.Language.Should().BeEmpty();
    }
}


