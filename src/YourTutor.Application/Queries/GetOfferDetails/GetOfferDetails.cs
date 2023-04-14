using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Queries.GetOfferDetails;

public sealed record GetOfferDetails(Guid id) : IRequest<OfferDetailsVm>;


