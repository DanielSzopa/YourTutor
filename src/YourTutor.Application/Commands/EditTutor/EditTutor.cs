using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Commands.EditTutor
{
    public sealed record EditTutor(EditTutorVm EditTutorVm, Guid UserId) : IRequest<Unit>;
}


