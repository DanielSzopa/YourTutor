using MediatR;
using YourTutor.Application.Abstractions.UserManager;

namespace YourTutor.Application.Commands.SignOut
{
    public class SignOutHandler : IRequestHandler<SignOut, Unit>
    {
        private readonly ISignOutManager _signOutManager;

        public SignOutHandler(ISignOutManager signOutManager)
        {
            _signOutManager = signOutManager;
        }
        public async Task<Unit> Handle(SignOut request, CancellationToken cancellationToken)
        {
            await _signOutManager.SignOutAsync();
            return Unit.Value;
        }
    }
}


