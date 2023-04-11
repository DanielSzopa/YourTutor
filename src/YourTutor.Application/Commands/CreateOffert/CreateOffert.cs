using MediatR;
using YourTutor.Application.ViewModels;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands.CreateOffert;

public sealed record CreateOffert(CreateOffertVm CreateOffertVm, UserId UserId) : IRequest<CreateOffertResponse>;



