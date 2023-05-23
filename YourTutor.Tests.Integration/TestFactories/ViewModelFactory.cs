using YourTutor.Application.ViewModels;

namespace YourTutor.Tests.Integration.TestFactories;

public static class ViewModelFactory
{

    public static RegisterVm ValidRegisterVm => new()
    {
        Email = "phill@gmail.com",
        FirstName = "Phill",
        LastName = "Cash",
        Password = "Test123!",
        PasswordConfirmation = "Test123!",
    };

    public static RegisterVm InvalidRegisterVm => new()
    {
        Email = "",
        FirstName = "Phill",
        LastName = "Cash",
        Password = "Test123!",
        PasswordConfirmation = "Test123!",
    };
}
