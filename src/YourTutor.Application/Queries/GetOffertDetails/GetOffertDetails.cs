using MediatR;

namespace YourTutor.Application.Queries.GetOffertDetails;

public sealed record GetOffertDetails(Guid id) : IRequest<GetOffertDetailsResponse>;


