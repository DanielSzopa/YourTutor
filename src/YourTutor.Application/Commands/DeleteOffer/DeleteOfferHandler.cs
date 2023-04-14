using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Abstractions.UnitOfWork;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Commands.DeleteOffer;

public sealed class DeleteOfferHandler : IRequestHandler<DeleteOffer, Unit>
{
    private readonly IOffertRepository _offertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOfferHandler> _logger;

    public DeleteOfferHandler(IOffertRepository offertRepository, IUnitOfWork unitOfWork,
        ILogger<DeleteOfferHandler> logger)
    {
        _offertRepository = offertRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOffer request, CancellationToken cancellationToken)
    {
        await _offertRepository.RemoveOffertById(request.Id);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Offert with id: {0} has been removed", request.Id.ToString());
        return Unit.Value;
    }
}


