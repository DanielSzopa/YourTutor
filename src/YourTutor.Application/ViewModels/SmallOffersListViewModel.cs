using YourTutor.Application.Dtos.Pagination;
using YourTutor.Application.Dtos;
using YourTutor.Core.ReadModels;

namespace YourTutor.Application.ViewModels;

public sealed record SmallOffersListViewModel(PaginationResponse<SmallOffersReadModel> PaginationResponse, OffersFilterDto OffersFilterDto);


