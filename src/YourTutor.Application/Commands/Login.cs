using MediatR;
using System.ComponentModel.DataAnnotations;
using YourTutor.Application.Dtos.Responses;

namespace YourTutor.Application.Commands
{
    public sealed class Login : IRequest<LoginResponse>
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}


