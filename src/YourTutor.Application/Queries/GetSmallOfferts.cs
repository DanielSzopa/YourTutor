using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;

namespace YourTutor.Application.Queries;

public sealed record GetSmallOfferts(PaginationDto paginationDto, OffertsFilterDto offertsDto);



