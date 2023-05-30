using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.ViewModels;
using YourTutor.Infrastructure.Security;

namespace YourTutor.Tests.Integration.TestFactories;

public static class ViewModelFactory
{
    private static readonly Faker _faker = new Faker();
    private static readonly string _password = "Test123!";

    public static RegisterVm ValidRegisterVm => new()
    {
        Email = _faker.Person.Email.ToLower(),
        FirstName = _faker.Person.FirstName,
        LastName = _faker.Person.LastName,
        Password = _password,
        PasswordConfirmation = _password
    };

    public static RegisterVm InvalidRegisterVm => new()
    {
        Email = "",
        FirstName = _faker.Person.FirstName,
        LastName = _faker.Person.LastName,
        Password = _password,
        PasswordConfirmation = _password
    };

    public static EditTutorVm EditTutorVm => new()
    {
        Country = _faker.Random.String2(6),
        Language = _faker.Random.String2(6),
        Description = _faker.Random.String2(6),
    };
}
