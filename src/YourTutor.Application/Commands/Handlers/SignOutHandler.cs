using MediatR;
using YourTutor.Core.Abstractions;

namespace YourTutor.Application.Commands.Handlers
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


