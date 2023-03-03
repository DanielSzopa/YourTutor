using MediatR;
using System.ComponentModel.DataAnnotations;

namespace YourTutor.Application.Commands;

public class Register : IRequest<Unit>
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be between 2 and 50 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be between 2 and 50 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 2 and 50 characters")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare(nameof(Register.Password), ErrorMessage = "Passwords must be the same")]
    public string PasswordConfirmation { get; set; }

}
