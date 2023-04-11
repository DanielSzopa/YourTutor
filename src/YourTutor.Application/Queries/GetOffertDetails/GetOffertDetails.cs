using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Queries.GetOffertDetails;

public sealed record GetOffertDetails(Guid id) : IRequest<OffertDetailsVm>;


