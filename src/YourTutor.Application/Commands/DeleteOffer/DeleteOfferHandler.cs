using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.DeleteOffer;

public sealed class DeleteOfferHandler : IRequestHandler<DeleteOffer, Unit>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOfferHandler> _logger;

    public DeleteOfferHandler(IOfferRepository offerRepository, IUnitOfWork unitOfWork,
        ILogger<DeleteOfferHandler> logger)
    {
        _offerRepository = offerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOffer request, CancellationToken cancellationToken)
    {
        await _offerRepository.RemoveOfferById(request.Id);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Offer with id: {0} has been removed", request.Id.ToString());
        return Unit.Value;
    }
}


