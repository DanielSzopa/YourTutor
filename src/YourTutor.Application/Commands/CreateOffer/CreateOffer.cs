using MediatR;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Commands.CreateOffer;

public sealed record CreateOffer(CreateOfferVm CreateOffertVm, Guid UserId) : IRequest<Guid>;



