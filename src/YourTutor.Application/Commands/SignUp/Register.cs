using MediatR;
using System.ComponentModel.DataAnnotations;
using YourTutor.Application.Dtos.Responses;

namespace YourTutor.Application.Commands.SignUp;

public sealed class Register : IRequest<RegisterResponse>
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; init; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be between 2 and 50 characters")]
    public string FirstName { get; init; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be between 2 and 50 characters")]
    public string LastName { get; init; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters")]
    public string Password { get; init; }

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare(nameof(Password), ErrorMessage = "Passwords must be the same")]
    public string PasswordConfirmation { get; init; }

}
