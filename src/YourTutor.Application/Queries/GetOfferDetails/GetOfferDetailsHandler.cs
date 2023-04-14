using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.ViewModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetOfferDetails;

public sealed class GetOfferDetailsHandler : IRequestHandler<GetOfferDetails, OfferDetailsVm>
{
    private readonly ILogger<GetOfferDetailsHandler> _logger;
    private readonly IOfferRepository _offerRepository;

    public GetOfferDetailsHandler(ILogger<GetOfferDetailsHandler> logger, IOfferRepository offerRepository)
    {
        _logger = logger;
        _offerRepository = offerRepository;
    }

    public async Task<OfferDetailsVm> Handle(GetOfferDetails request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetOfferDetails(request.id);

        if (offer == null)
        {
            _logger.LogError("Can not find offer with id: {0}", request.id);
            return default;
        }

        var (id, description, subject, price, location, isRemotely, fullName,
            email, country, speakingLang, tutorId) = offer;

        return new OfferDetailsVm(id, description, subject, price, location, isRemotely, fullName, email, country, speakingLang, tutorId);
    }
}


