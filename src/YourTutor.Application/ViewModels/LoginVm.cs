using System.ComponentModel.DataAnnotations;

namespace YourTutor.Application.ViewModels;

public sealed class LoginVm
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }

    public bool RememberMe { get; init; }
}


