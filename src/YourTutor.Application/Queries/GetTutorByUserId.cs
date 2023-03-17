using MediatR;
using YourTutor.Application.Dtos.Tutor;

namespace YourTutor.Application.Queries
{
    public sealed record GetTutorByUserId(Guid UserId) : IRequest<TutorDto>;
}


