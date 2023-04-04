using MediatR;
using YourTutor.Application.Dtos;
using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.Dtos.Responses;

namespace YourTutor.Application.Queries;

public sealed record GetSmallOfferts(PaginationDto paginationDto, OffertsFilterDto offertsDto) : IRequest<GetSmallOffertsResponse>;



