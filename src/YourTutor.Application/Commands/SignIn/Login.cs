﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace YourTutor.Application.Commands.SignIn
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


