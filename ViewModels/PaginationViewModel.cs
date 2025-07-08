namespace NewsApi.ViewModels;

public class PaginationViewModel<T>
{
     public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public IEnumerable<T> Data { get; set; } = new List<T>();
}
