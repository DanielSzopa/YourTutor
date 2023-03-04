﻿using MediatR;
using Microsoft.Extensions.Options;
using YourTutor.Application.Abstractions;
using YourTutor.Application.Dtos;
using YourTutor.Application.Models.EmailBase;
using YourTutor.Core.Abstractions;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Entities;
using YourTutor.Core.ValueObjects;
using YourTutor.Infrastructure.Settings;

namespace YourTutor.Application.Commands.Handlers
{
    public class RegisterHandler : IRequestHandler<Register, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISignInManager _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly EmailSettings _emailSettings;

        public RegisterHandler(IUserRepository userRepository, ISignInManager signInManager,
            IEmailSender emailSender, IOptions<EmailSettings> emailSettings)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _emailSettings = emailSettings.Value;
        }

        public async Task<RegisterResponse> Handle(Register command, CancellationToken cancellationToken)
        {
            var response = new RegisterResponse();
            if (await _userRepository.IsEmailAlreadyExists(command.Email))
                response.Errors.Add($"Email already exists: {command.Email}");

            User user = null;

            try
            {
                user = new User(Guid.NewGuid(), command.Email, command.FirstName, command.LastName, (Password)command.Password);
                user.Register(command.Password);
            }
            catch (Exception ex) 
            {
                response.Errors.Add(ex.Message);
            }

            if (response.Errors.Count > 0)
                return response;

            await _userRepository.AddUser(user);
            await _signInManager.SignInAsync(false, user.Id);

            if (_emailSettings.RegistrationNotificationIsEnabled)
                await _emailSender.SendEmail(new RegisterEmail(user.Email, _emailSettings.From));

            return response;
        }
    }
}


