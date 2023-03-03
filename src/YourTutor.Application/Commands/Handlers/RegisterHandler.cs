using MediatR;
using YourTutor.Core.Abstractions;
using YourTutor.Core.Abstractions.Repositories;
using YourTutor.Core.Entities;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands.Handlers
{
    public class RegisterHandler : IRequestHandler<Register, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISignInManager _signInManager;

        public RegisterHandler(IUserRepository userRepository, ISignInManager signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(Register command, CancellationToken cancellationToken)
        {
            var user = new User(Guid.NewGuid(), command.Email, command.FirstName, command.LastName, (Password)command.Password);
            user.Register(command.Password);

            await _userRepository.AddUser(user);

            await _signInManager.SignInAsync(false, user.Id);

            return Unit.Value;
        }
    }
}


