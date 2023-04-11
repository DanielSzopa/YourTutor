using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Commands.CreateOffert;

public sealed record CreateOffert(CreateOffertVm CreateOffertVm, Guid UserId) : IRequest<Guid>;



