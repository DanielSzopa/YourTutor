using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.ViewModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetOffertDetails;

public sealed class GetOffertDetailsHandler : IRequestHandler<GetOffertDetails, OffertDetailsVm>
{
    private readonly ILogger<GetOffertDetailsHandler> _logger;
    private readonly IOffertRepository _offertRepository;

    public GetOffertDetailsHandler(ILogger<GetOffertDetailsHandler> logger, IOffertRepository offertRepository)
    {
        _logger = logger;
        _offertRepository = offertRepository;
    }

    public async Task<OffertDetailsVm> Handle(GetOffertDetails request, CancellationToken cancellationToken)
    {
        var offert = await _offertRepository.GetOffertDetails(request.id);

        if (offert == null)
        {
            _logger.LogError("Can not find offert with id: {0}", request.id);
            return default;
        }

        var (id, description, subject, price, location, isRemotely, fullName,
            email, country, speakingLang, tutorId) = offert;

        return new OffertDetailsVm(id, description, subject, price, location, isRemotely, fullName, email, country, speakingLang, tutorId);
    }
}


