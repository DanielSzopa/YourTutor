using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YourTutor.Application.Abstractions.Email;
using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Abstractions.UserManager;
using YourTutor.Application.Helpers;
using YourTutor.Application.Models.EmailBase;
using YourTutor.Application.Settings.Email;
using YourTutor.Core.Entities;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands.SignUp
{
    public class RegisterHandler : IRequestHandler<Register, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISignInManager _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHashService _hashService;
        private readonly ILogger<RegisterHandler> _logger;
        private readonly EmailSettings _emailSettings;

        public RegisterHandler(IUserRepository userRepository, ISignInManager signInManager,
            IEmailSender emailSender, IOptions<EmailSettings> emailSettings, IHashService hashService,
            ILogger<RegisterHandler> logger)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _hashService = hashService;
            _logger = logger;
            _emailSettings = emailSettings.Value;
        }

        public async Task<RegisterResponse> Handle(Register command, CancellationToken cancellationToken)
        {
            var registerVm = command.RegisterVm;
            var response = new RegisterResponse();
            if (await _userRepository.IsEmailAlreadyExistsAsync(registerVm.Email))
            {
                response.Errors.Add($"Email already exists: {registerVm.Email}");
                _logger.LogError(AppLogEvent.SignUp, "Problem with registering user, email already exists, email {@email}", registerVm.Email);
            }


            User user = null;

            try
            {
                var password = new Password(registerVm.Password);
                password.CheckIfPasswordsMatch(registerVm.PasswordConfirmation);

                var hashPassword = new HashPassword(_hashService.HashPassword(password));

                user = new User(Guid.NewGuid(), registerVm.Email, registerVm.FirstName, registerVm.LastName, hashPassword);

                user.CreateTutor();
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                _logger.LogError(AppLogEvent.SignUp, ex, "Problem with registering user");
            }

            if (response.Errors.Count > 0)
                return response;

            await _userRepository.AddUserAsync(user);
            await _signInManager.SignInAsync(false, user.Id, $"{user.FirstName.Value} {user.LastName.Value}");

            if (_emailSettings.RegistrationNotificationIsEnabled)
                await _emailSender.SendEmailAsync(new RegisterEmail(user.Email, _emailSettings.From));

            return response;
        }
    }
}


