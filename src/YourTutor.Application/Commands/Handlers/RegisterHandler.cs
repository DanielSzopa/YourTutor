using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Core.Abstractions;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Entities;
using YourTutor.Core.Exceptions;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands.Handlers
{
    public class RegisterHandler : IRequestHandler<Register, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISignInManager _signInManager;

        public RegisterHandler(IUserRepository userRepository, ISignInManager signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
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

            return response;
        }
    }
}


