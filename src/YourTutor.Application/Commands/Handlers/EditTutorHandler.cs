using MediatR;

namespace YourTutor.Application.Commands.Handlers;

public sealed class EditTutorHandler : IRequestHandler<EditTutor, Unit>
{
    public Task<Unit> Handle(EditTutor request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


