namespace YourTutor.Application.Dtos.Pagination;

public sealed class PaginationResponse<T>
    where T : class
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public int ItemsFrom { get; }
    public int ItemsTo { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public string SearchString { get; }
    public IReadOnlyCollection<T> Items { get; }

    public PaginationResponse(IReadOnlyCollection<T> items, int pageNumber, int pageSize, int totalCount, string searchString)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        SearchString = searchString;
    }
}


