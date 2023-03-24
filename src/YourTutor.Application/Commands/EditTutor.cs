using MediatR;
using YourTutor.Application.Dtos;

namespace YourTutor.Application.Commands
{
    public sealed record EditTutor(EditTutorDto Dto, Guid UserId) : IRequest<Unit>;
}


