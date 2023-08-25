using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Abstractions.UserManager;
using YourTutor.Application.Helpers;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.SignIn
{
    public sealed class LoginHandler : IRequestHandler<Login, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISignInManager _signInManager;
        private readonly IHashService _hashService;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(IUserRepository userRepository, ISignInManager signInManager, IHashService hashService, ILogger<LoginHandler> logger)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _hashService = hashService;
            _logger = logger;
        }

        public async Task<LoginResponse> Handle(Login request, CancellationToken cancellationToken)
        {
            var loginResponse = new LoginResponse();
            var user = await _userRepository.GetUserByEmailAsync(request.LoginVm.Email.ToLower(), cancellationToken);
            if (user is null)
            {
                loginResponse.Errors.Add("Invalid credentials");
                _logger.LogError(AppLogEvent.SignIn, "Can not found user {@email}", request.LoginVm.Email);
                return loginResponse;
            }

            bool verifyPasswordResult = false;
            try
            {
                verifyPasswordResult = _hashService.VerifyPassword(request.LoginVm.Password, user.HashPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(AppLogEvent.SignIn,"Problem with verify password, hashedPassword: {0} , inputPassword: {1}, exception: {2}",
                    user.HashPassword.Value, request.LoginVm.Password, ex.ToString());
            }
            

            if (verifyPasswordResult is false)
            {
                loginResponse.Errors.Add("Invalid credentials");
                _logger.LogError(AppLogEvent.SignIn, "Password is incorrect for: {@email}", request.LoginVm.Email);
                return loginResponse;
            }

            await _signInManager.SignInAsync(request.LoginVm.RememberMe, user.Id, $"{user.FirstName.Value} {user.LastName.Value}");

            return loginResponse;
        }
    }
}


