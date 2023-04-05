using MediatR;
using Microsoft.AspNetCore.Authorization;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands.Handlers;

public sealed class DeleteOffertHandler : IRequestHandler<DeleteOffert, Unit>
{
    public DeleteOffertHandler()
    {
        
    }

    public async Task<Unit> Handle(DeleteOffert request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }
}


