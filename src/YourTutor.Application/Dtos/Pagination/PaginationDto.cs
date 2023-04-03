namespace YourTutor.Application.Dtos.Pagination;

public sealed record PaginationDto(int PageNumber, string SearchString, string OrderBy, bool isDescending);


