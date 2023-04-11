using MediatR;
using YourTutor.Application.ViewModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands.CreateOffert;

public sealed record CreateOffert(CreateOffertVm CreateOffertVm, Guid UserId) : IRequest<CreateOffertResponse>;



