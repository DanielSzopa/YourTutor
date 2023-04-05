using MediatR;

namespace YourTutor.Application.Commands.DeleteOffert;

public sealed record DeleteOffert(Guid Id) : IRequest<Unit>;


