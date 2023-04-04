using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.Dtos.Responses;
using YourTutor.Core.ReadModels;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Queries.Handlers;

public sealed class GetSmallOffertsHandler : IRequestHandler<GetSmallOfferts, GetSmallOffertsResponse>
{
    private readonly int _pageSize = 10;
    private readonly IOffertRepository _offertRepository;

    public GetSmallOffertsHandler(IOffertRepository offertRepository)
    {
        _offertRepository = offertRepository;
    }

    public async Task<GetSmallOffertsResponse> Handle(GetSmallOfferts request, CancellationToken cancellationToken)
    {
        var (pagination, offert) = request;
        var query = _offertRepository.GetOffertsAsQueryable();
        var searchString = pagination?.SearchString?.ToLower();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query
                .Where(o => o.Subject.ToLower().Contains(searchString)
                || o.Location.ToLower().Contains(searchString)
                || ((string)o.Tutor.User.Email).ToLower().Contains(searchString)
                || ((string)o.Tutor.User.FirstName).ToLower().Contains(searchString)
                || ((string)o.Tutor.User.LastName).ToLower().Contains(searchString));
        }

        query = query
            .Where(o => o.IsRemotely == offert.IsRemotely);

        if (offert.PriceFrom > 0 && offert.PriceTo > 0)
        {
            query = query
               .Where(o => o.Price >= new Price(offert.PriceFrom) && o.Price <= new Price(offert.PriceTo));
        }

        var quantity = await _offertRepository.CountOfferts(query);

        query = query
            .Skip(ExcludeRecords(pagination.PageNumber, _pageSize))
            .Take(_pageSize);

        var items = await _offertRepository
            .GetSmallOfferts(query);

        var results = items
            .OrderBy(o => o.FullName)
            .ToList();

        var paginationResponse = new PaginationResponse<SmallOffertsReadModel>(results, pagination.PageNumber, _pageSize, quantity, pagination?.SearchString);
        var filter = new OffertsFilterDto(offert.IsRemotely, offert.PriceFrom, offert.PriceTo);

        return new GetSmallOffertsResponse(paginationResponse, filter);
    }


    private int ExcludeRecords(int pageNumber, int pageSize)
        => pageSize * (pageNumber - 1);
}


