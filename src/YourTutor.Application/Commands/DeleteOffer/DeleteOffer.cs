using MediatR;

namespace YourTutor.Application.Commands.DeleteOffer;

public sealed record DeleteOffer(Guid Id) : IRequest<Unit>;


