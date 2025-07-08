using NewsApi.Models;
using NewsApi.ViewModels;

namespace NewsApi.Services.Interfaces;

public interface INewsService
{
    Task<NewsViewModel?> GetByIdAsync(int id);
    Task<NewsViewModel> CreateAsync(NewsPostViewModel model);
    Task<bool> UpdateAsync(int id, NewsPutViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<NewsViewModel>> SearchAsync(string? query);
}
