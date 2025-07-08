namespace NewsApi.ViewModels;

public class NewsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime Date { get; set; }
}
