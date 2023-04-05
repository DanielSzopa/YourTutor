using MediatR;
using YourTutor.Core.ReadModels;

namespace YourTutor.Application.Queries.GetTutorByUserId
{
    public sealed record GetTutorByUserId(Guid UserId) : IRequest<TutorDetailsReadModel>;
}


