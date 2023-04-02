using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Commands;

public sealed record CreateOffert(CreateOffertDto Dto, UserId UserId) : IRequest<OffertId>;



