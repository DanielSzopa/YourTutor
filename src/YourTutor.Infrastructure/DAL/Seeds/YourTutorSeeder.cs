using Bogus;
using Microsoft.Extensions.Options;
using YourTutor.Application.Abstractions.Security;
using YourTutor.Core.Entities;
using YourTutor.Core.ValueObjects;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Infrastructure.DAL.Seeds;

public sealed class YourTutorSeeder : IYourTutorSeeder
{
    private readonly IHashService _hashService;
    private readonly SeederSettings _settings;

    public YourTutorSeeder(IHashService hashService, IOptions<SeederSettings> settings)
    {
        _hashService = hashService;
        _settings = settings.Value;
    }
    public IReadOnlyCollection<User> GetSeedData()
    {
        var users = new List<User>();

        for (int i = 0; i < _settings.Quantity; i++)
        {
            var faker = new Faker(_settings.Locale);
            var password = new Password(_settings.Password);
            var hashPassword = new HashPassword(_hashService.HashPassword(password));

            var user = new User(Guid.NewGuid(), faker.Person.Email, faker.Person.FirstName, faker.Person.LastName, hashPassword);

            user.CreateTutor();
            user.Tutor.UpdateCountry(faker.PickRandom(GetCountries()));
            user.Tutor.UpdateDescription(faker.PickRandom(GetTutorDescriptions()));
            user.Tutor.UpdateLanguage(faker.PickRandom(GetLanguages()));

            user.Tutor.AddOffer(new Offer(Guid.NewGuid(), faker.PickRandom(GetOffersDescriptions()), faker.PickRandom(GetSubjects()), faker.Random.Number(50, 150), faker.Random.Bool(), faker.PickRandom(GetLocations()), user.Id));
            users.Add(user);
        }

        return users;
    }

    private string[] GetTutorDescriptions() => new[]
    {
                "Jestem nauczycielem z dziesięcioletnim stażem, pragnę przekazać ci wiedzę",
                "Nauczanie jest moją pasją, posiadam doświadczenie, wiedzę oraz chęć do przekazania tobie wiedzy",
                "Moje doświadczenie: Praca w przedszkolu, szkole oraz prywatne udzielanie korepetycji",
                "Moje kompetencje: Język angielski, polski, matematyka, fizyka, programowanie"
    };

    private string[] GetOffersDescriptions() => new[]
    {
                "Na moich zajęciach, przekazywana wiedza jest z mojego zawodowego doświadczenia",
                "Prowadzę zajęcia w miłej atmosferze, moim zadaniem jest abyś na zajęciach czuł się jak w domu",
                "Nauka nie musi być nudna, możemy się uczyć przez zabawę"
    };

    private string[] GetCountries() => new[]
    {
                "Polska"
    };

    private string[] GetLocations() => new[]
    {
                "Warszawa",
                "Kraków"
    };

    private string[] GetLanguages() => new[]
    {
                "Angielski",
                "Polski"
    };

    private string[] GetSubjects() => new[]
    {
                "Język Polski",
                "Matematyka",
                "Programowanie",
                "Chemia",
                "Fizyka"
    };

}


