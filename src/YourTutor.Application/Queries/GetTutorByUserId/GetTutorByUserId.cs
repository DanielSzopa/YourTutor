using MediatR;

namespace YourTutor.Application.Queries.GetTutorByUserId
{
    public sealed record GetTutorByUserId(Guid UserId) : IRequest<GetTutorByUserIdResponse>;
}


