namespace Shared.Common.Lists;

public class PaginatedList<T> where T : class
{
    //Properties
    public List<T> Items { get; }

    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int CurrentPage { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    //Construction
    public PaginatedList(List<T> items, int count, int currentPage, int pageSize =10)
    {
        Items = items;
        TotalCount = count;
        CurrentPage = currentPage;
        PageSize = pageSize;
        
        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
