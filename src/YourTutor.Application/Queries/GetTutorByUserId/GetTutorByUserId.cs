using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Queries.GetTutorByUserId
{
    public sealed record GetTutorByUserId(Guid UserId) : IRequest<TutorDetailsVm>;
}


