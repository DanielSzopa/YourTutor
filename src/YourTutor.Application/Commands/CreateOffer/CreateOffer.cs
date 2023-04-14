using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Commands.CreateOffer;

public sealed record CreateOffer(CreateOfferVm CreateOfferVm, Guid UserId) : IRequest<Guid>;



