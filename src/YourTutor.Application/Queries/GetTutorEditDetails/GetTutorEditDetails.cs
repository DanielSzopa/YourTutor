using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Queries.GetTutorEditDetails;

public sealed record GetTutorEditDetails(Guid TutorId) : IRequest<EditTutorVm>;


