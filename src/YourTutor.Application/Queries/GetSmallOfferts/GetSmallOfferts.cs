using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Queries.GetSmallOfferts;

public sealed record GetSmallOfferts(PaginationDto paginationDto, OffertsFilterDto offertsDto) : IRequest<SmallOffertsListViewModel>;



