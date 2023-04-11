using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.Dtos.Responses;
using YourTutor.Core.ReadModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetSmallOfferts;

public sealed class GetSmallOffertsHandler : IRequestHandler<GetSmallOfferts, GetSmallOffertsResponse>
{
    private readonly int _pageSize = 10;
    private readonly int _defaultPage = 1;

    private readonly IOffertRepository _offertRepository;

    public GetSmallOffertsHandler(IOffertRepository offertRepository)
    {
        _offertRepository = offertRepository;
    }

    public async Task<GetSmallOffertsResponse> Handle(GetSmallOfferts request, CancellationToken cancellationToken)
    {
        var (pagination, offert) = request;
        pagination = pagination.PageNumber == 0
            ? pagination with { PageNumber = _defaultPage }
            : pagination;

        var smallOffersGroup = await _offertRepository.GetSmallOfferts(offert.IsRemotely, offert.IsRemotelyFiltered, offert.PriceFrom, offert.PriceTo,
            _pageSize, ExcludeRecords(pagination.PageNumber, _pageSize), pagination.SearchString);


        var results = smallOffersGroup.Offerts
            .OrderBy(o => o.FullName)
            .ToList();

        var paginationResponse = new PaginationResponse<SmallOffertsReadModel>(results, pagination.PageNumber, _pageSize, smallOffersGroup.Count, pagination?.SearchString);
        var filter = new OffertsFilterDto(offert.IsRemotely, offert.IsRemotelyFiltered, offert.PriceFrom, offert.PriceTo);

        return new GetSmallOffertsResponse(paginationResponse, filter);
    }

    private int ExcludeRecords(int pageNumber, int pageSize)
        => pageSize * (pageNumber - 1);
}


