using MediatR;

namespace YourTutor.Application.Commands.SignOut;

public sealed record SignOut : IRequest<Unit>;


