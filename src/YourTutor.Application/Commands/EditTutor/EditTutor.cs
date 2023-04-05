using MediatR;
using YourTutor.Application.Dtos;

namespace YourTutor.Application.Commands.EditTutor
{
    public sealed record EditTutor(EditTutorDto Dto, Guid UserId) : IRequest<Unit>;
}


