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
                "I am a teacher with ten years of experience, I would like to give you knowledge",
                "Teaching is my passion, I have the experience, knowledge and willingness to give you the knowledge",
                "My experience: Working in kindergarten, school and private tutoring",
                "My competencies: English, Polish, math, physics, programming"
    };

    private string[] GetOffersDescriptions() => new[]
    {
                "In my classes, the knowledge transferred is from my professional experience",
                "I conduct classes in a relaxed atmosphere, my job is to make you feel at home in my classes",
                "Learning doesn't have to be boring, we can learn through fun"
    };

    private string[] GetCountries() => new[]
    {
                "Poland"
    };

    private string[] GetLocations() => new[]
    {
                "Warsaw",
                "Krakow"
    };

    private string[] GetLanguages() => new[]
    {
                "English",
                "Polish"
    };

    private string[] GetSubjects() => new[]
    {
                "Polish language",
                "Math",
                "Programming",
                "Chemistry",
                "Physics"
    };

}


