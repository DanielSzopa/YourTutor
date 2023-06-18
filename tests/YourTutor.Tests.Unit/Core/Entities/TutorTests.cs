using FluentAssertions.Execution;
using YourTutor.Core.Entities;
using YourTutor.Tests.Unit.Fixtures;
using YourTutor.Tests.Unit.TestFactories;

namespace YourTutor.Tests.Unit.Core.Entities;

public class TutorTests : IClassFixture<FakerFixture>
{
    private readonly Faker _faker;

    public TutorTests(FakerFixture fakerFixture)
    {
        _faker = fakerFixture.Faker;
    }

    [Fact]
    public void Create_WithValidProperties_ShouldNotThrowAnyException()
    {
        //act
        Action result = () =>
        {
            new Tutor(Guid.NewGuid(), _faker.Random.String2(10),
            _faker.Random.String2(10), _faker.Random.String2(10));
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
            new Tutor(Guid.Empty, _faker.Random.String2(10),
            _faker.Random.String2(10), _faker.Random.String2(10));
        };

        //assert
        result.Should().Throw<Exception>();
    }

    [Fact]
    public void UpdateDescription_ShouldUpdateDescription()
    {
        //arrange
        var description = _faker.Random.String2(12);
        var tutor = TestTutorFactory.Tutor;

        //act
        tutor.UpdateDescription(description);

        //assert
        tutor.Description.Should().Be(description);
    }

    [Fact]
    public void UpdateCountry_ShouldUpdateCountry()
    {
        //arrange
        var country = _faker.Random.String2(12);
        var tutor = TestTutorFactory.Tutor;

        //act
        tutor.UpdateCountry(country);

        //assert
        tutor.Country.Should().Be(country);
    }

    [Fact]
    public void UpdateLanguage_ShouldUpdateLanguage()
    {
        //arrange
        var language = _faker.Random.String2(12);
        var tutor = TestTutorFactory.Tutor;

        //act
        tutor.UpdateLanguage(language);

        //assert
        tutor.Language.Should().Be(language);
    }

    [Fact]
    public void AddOffer_ShouldAddOffer()
    {
        //arrange
        var offer = TestOfferFactory.Offer;
        var tutor = TestTutorFactory.Tutor;

        //act
        tutor.AddOffer(offer);

        //assert
        var offers = tutor.Offers;

        using var scope = new AssertionScope();
        offers.Count.Should().Be(1);
    }
}


