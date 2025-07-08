namespace NewsApi.ViewModels.Filters;

public class PaginationFilterViewModel
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Query { get; set; } 
}
