namespace NewsApi.ViewModels;

public class NewsPutViewModel
{
    public int Id { get; set; } 
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Author { get; set; } = null!;
}
