using NewsApi.Models;
using NewsApi.Repositories.Interfaces;
using NewsApi.Services.Interfaces;
using NewsApi.ViewModels;
using NewsApi.ViewModels.Filters;

namespace NewsApi.Services;

public class NewsService : INewsService, IPaginatedService<News, NewsViewModel>
{
    private readonly INewsRepository _repository;

    public NewsService(INewsRepository repository)
    {
        _repository = repository;
    }

    public async Task<NewsViewModel?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : ToViewModel(entity);
    }

    public async Task<NewsViewModel> CreateAsync(NewsPostViewModel model)
    {
        var entity = new News
        {
            Title = model.Title,
            Body = model.Body,
            ImageUrl = model.ImageUrl,
            Author = model.Author,
            Date = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
        return ToViewModel(entity);
    }

    public async Task<bool> UpdateAsync(int id, NewsPutViewModel model)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        existing.Title = model.Title;
        existing.Body = model.Body;
        existing.ImageUrl = model.ImageUrl;
        existing.Author = model.Author;

        await _repository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        await _repository.DeleteAsync(existing);
        return true;
    }

    public async Task<IEnumerable<NewsViewModel>> SearchAsync(string? query)
    {
        var results = await _repository.SearchAsync(query ?? "");
        return results.Select(ToViewModel);
    }

    public async Task<PaginationViewModel<NewsViewModel>> GetPaginatedAsync(PaginationFilterViewModel filter)
    {
        var query = await _repository.GetAllAsync(filter.Query);

        var totalItems = query.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)filter.PageSize);

        var items = query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(ToViewModel)
            .ToList();

        return new PaginationViewModel<NewsViewModel>
        {
            CurrentPage = filter.Page,
            PageSize = filter.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Data = items
        };
    }

    private static NewsViewModel ToViewModel(News n) => new()
    {
        Id = n.Id,
        Title = n.Title,
        Author = n.Author,
        Date = n.Date,
        ImageUrl = n.ImageUrl,
        Body= n.Body,
    };
}
