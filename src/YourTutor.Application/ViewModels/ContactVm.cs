using System.ComponentModel.DataAnnotations;

namespace YourTutor.Application.ViewModels;

public class ContactVm
{
    public Guid Id { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string To { get; init; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(350, MinimumLength = 2, ErrorMessage = "Must be between 2 and 350 characters")]
    public string Description { get; init; }

}


