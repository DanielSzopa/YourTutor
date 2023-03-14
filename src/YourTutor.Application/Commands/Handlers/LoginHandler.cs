using MediatR;
using YourTutor.Application.Abstractions.Security;
using YourTutor.Application.Abstractions.UserManager;
using YourTutor.Application.Dtos;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.Handlers
{
    public sealed class LoginHandler : IRequestHandler<Login, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISignInManager _signInManager;
        private readonly IHashService _hashService;

        public LoginHandler(IUserRepository userRepository, ISignInManager signInManager, IHashService hashService)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _hashService = hashService;
        }

        public async Task<LoginResponse> Handle(Login request, CancellationToken cancellationToken)
        {
            var loginResponse = new LoginResponse();
            var user = await _userRepository.GetUserByEmail(request.Email.ToLower());
            if (user is null)
            {
                loginResponse.Errors.Add("Invalid credentials");
                return loginResponse;
            }

            var result = _hashService.VerifyPassword(request.Password, user.HashPassword);

            if(result is false)
            {
                loginResponse.Errors.Add("Invalid credentials");
                return loginResponse;
            }

            await _signInManager.SignInAsync(request.RememberMe, user.Id, $"{user.FirstName.Value} {user.LastName.Value}");

            return loginResponse;
        }
    }
}


