using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.ViewModels;
using YourTutor.Core.ReadModels;
using YourTutor.Core.Repositories;

namespace YourTutor.Application.Queries.GetSmallOffers;

public sealed class GetSmallOffersHandler : IRequestHandler<GetSmallOffers, SmallOffersListViewModel>
{
    private readonly int _pageSize = 10;
    private readonly int _defaultPage = 1;

    private readonly IOfferRepository _offerRepository;

    public GetSmallOffersHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<SmallOffersListViewModel> Handle(GetSmallOffers request, CancellationToken cancellationToken)
    {
        var (pagination, offer) = request;
        pagination = pagination.PageNumber == 0
            ? pagination with { PageNumber = _defaultPage }
            : pagination;

        var smallOffersGroup = await _offerRepository.GetSmallOffers(offer.IsRemotely, offer.IsRemotelyFiltered, offer.PriceFrom, offer.PriceTo,
            _pageSize, ExcludeRecords(pagination.PageNumber, _pageSize), pagination.SearchString);


        var results = smallOffersGroup.Offers
            .OrderBy(o => o.FullName)
            .ToList();

        var paginationResponse = new PaginationResponse<SmallOffersReadModel>(results, pagination.PageNumber, _pageSize, smallOffersGroup.Count, pagination?.SearchString);
        var filter = new OffersFilterDto(offer.IsRemotely, offer.IsRemotelyFiltered, offer.PriceFrom, offer.PriceTo);

        return new SmallOffersListViewModel(paginationResponse, filter);
    }

    private int ExcludeRecords(int pageNumber, int pageSize)
        => pageSize * (pageNumber - 1);
}


