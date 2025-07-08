using NewsApi.Models;

namespace NewsApi.Repositories.Interfaces;

public interface INewsRepository
{
    Task<IEnumerable<News>> GetAllAsync(string? query);
    Task<News?> GetByIdAsync(int id);
    Task<IEnumerable<News>> SearchAsync(string query);
    Task AddAsync(News news);
    Task UpdateAsync(News news);
    Task DeleteAsync(News news);
    Task<(IEnumerable<News> Items, int TotalItems)> GetPagedAsync(int page, int pageSize);
}
