using YourTutor.Application.Dtos.Pagination;
using YourTutor.Core.ReadModels;

namespace YourTutor.Application.Dtos.Responses;

public sealed record GetSmallOffertsResponse(PaginationResponse<SmallOffertsReadModel> PaginationResponse, OffertsFilterDto OffertsFilterDto);


