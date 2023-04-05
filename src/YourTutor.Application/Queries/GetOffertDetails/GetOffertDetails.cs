using MediatR;
using YourTutor.Core.ReadModels;

namespace YourTutor.Application.Queries.GetOffertDetails;

public sealed record GetOffertDetails(Guid id) : IRequest<OffertDetailsReadmodel>;


