using MediatR;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Entities;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.CreateOffer
{
    public sealed class CreateOfferHandler : IRequestHandler<CreateOffer, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfferRepository _offerRepository;

        public CreateOfferHandler(IUnitOfWork unitOfWork, IOfferRepository offerRepository)
        {
            _unitOfWork = unitOfWork;
            _offerRepository = offerRepository;
        }

        public async Task<Guid> Handle(CreateOffer request, CancellationToken cancellationToken)
        {
            var vm = request.CreateOfferVm;
            var offer = new Offer(Guid.NewGuid(), vm.Description, vm.Subject, vm.Price, vm.IsRemotely, vm.Location, request.UserId);

            await _offerRepository.CreateOffer(offer);
            await _unitOfWork.SaveChangesAsync();

            return offer.Id;
        }
    }
}


