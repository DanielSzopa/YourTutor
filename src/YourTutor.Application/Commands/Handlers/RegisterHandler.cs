using MediatR;

namespace YourTutor.Application.Commands.Handlers
{
    public class RegisterHandler : IRequestHandler<Register, Unit>
    {
        public Task<Unit> Handle(Register request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


