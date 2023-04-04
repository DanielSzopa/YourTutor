using MediatR;
using YourTutor.Core.Repositories;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Application.Queries.Handlers;

public sealed class GetSmallOffertsHandler : IRequestHandler<GetSmallOfferts, Unit>
{
    private readonly int _pageSize = 10;
    private readonly IOffertRepository _offertRepository;

    public GetSmallOffertsHandler(IOffertRepository offertRepository)
    {
        _offertRepository = offertRepository;
    }

    public async Task<Unit> Handle(GetSmallOfferts request, CancellationToken cancellationToken)
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

        query = query
            .Skip(ExcludeRecords(pagination.PageNumber, _pageSize))
            .Take(_pageSize);

        var result = await _offertRepository.GetSmallOfferts(query);

        return Unit.Value;
    }


    private int ExcludeRecords(int pageNumber, int pageSize)
        => pageSize * (pageNumber - 1);
}


