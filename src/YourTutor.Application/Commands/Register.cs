using MediatR;
using System.ComponentModel.DataAnnotations;

namespace YourTutor.Application.Commands;

public record Register(string Email, string FirstName, string LastName, string Password, string PasswordConfirmation) : IRequest<Unit>
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "EmailAddress is invalid")]
    public string Email { get; init; } = Email;

    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be between 2 and 50 characters")]
    public string FirstName { get; init; } = FirstName;

    [Required(ErrorMessage = "LastName is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be between 2 and 50 characters")]
    public string LastName { get; init; } = LastName;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be between 2 and 50 characters")]
    public string Password { get; init; } = Password;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare(nameof(Register.Password), ErrorMessage = "Passwords must be the same")]
    public string PasswordConfirmation { get; init; } = PasswordConfirmation;

}
