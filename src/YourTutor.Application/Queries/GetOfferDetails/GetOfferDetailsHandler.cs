using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.ViewModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetOfferDetails;

public sealed class GetOfferDetailsHandler : IRequestHandler<GetOfferDetails, OfferDetailsVm>
{
    private readonly ILogger<GetOfferDetailsHandler> _logger;
    private readonly IOffertRepository _offertRepository;

    public GetOfferDetailsHandler(ILogger<GetOfferDetailsHandler> logger, IOffertRepository offertRepository)
    {
        _logger = logger;
        _offertRepository = offertRepository;
    }

    public async Task<OfferDetailsVm> Handle(GetOfferDetails request, CancellationToken cancellationToken)
    {
        var offert = await _offertRepository.GetOffertDetails(request.id);

        if (offert == null)
        {
            _logger.LogError("Can not find offert with id: {0}", request.id);
            return default;
        }

        var (id, description, subject, price, location, isRemotely, fullName,
            email, country, speakingLang, tutorId) = offert;

        return new OfferDetailsVm(id, description, subject, price, location, isRemotely, fullName, email, country, speakingLang, tutorId);
    }
}


