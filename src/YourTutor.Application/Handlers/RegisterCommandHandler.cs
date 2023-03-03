using MediatR;
using YourTutor.Application.Commands;

namespace YourTutor.Application.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        public Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


