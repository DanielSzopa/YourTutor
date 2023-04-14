using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.ViewModels;

namespace YourTutor.Application.Queries.GetSmallOffers;

public sealed record GetSmallOffers(PaginationDto paginationDto, OffersFilterDto OffersDto) : IRequest<SmallOffersListViewModel>;



