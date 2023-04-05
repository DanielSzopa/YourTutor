using MediatR;

namespace YourTutor.Application.Commands;

public sealed record DeleteOffert(Guid Id) : IRequest<Unit>;


