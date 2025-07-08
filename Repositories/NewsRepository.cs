using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;

namespace NewsApi.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly AppDbContext _context;

    public NewsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<News>> GetAllAsync(string? query)
    {
        var newsQuery = _context.News.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var normalizedQuery = query.Trim().ToLower();

            newsQuery = newsQuery.Where(n =>
                n.Title.ToLower().Contains(normalizedQuery) ||
                n.Author.ToLower().Contains(normalizedQuery) ||
                n.Body.ToLower().Contains(normalizedQuery)
            );
        }

        return await newsQuery
            .OrderByDescending(n => n.Date)
            .ToListAsync();
    }

    public async Task<News?> GetByIdAsync(int id) =>
        await _context.News.FindAsync(id);

    public async Task<IEnumerable<News>> SearchAsync(string query) =>
        await _context.News
            .Where(n => n.Title.Contains(query) || n.Author.Contains(query))
            .ToListAsync();

    public async Task AddAsync(News news)
    {
        Console.WriteLine($"News => {System.Text.Json.JsonSerializer.Serialize(news)}");
        _context.News.Add(news);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(News news)
    {
        _context.Entry(news).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(News news)
    {
        _context.News.Remove(news);
        await _context.SaveChangesAsync();
    }
    public async Task<(IEnumerable<News> Items, int TotalItems)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.News.OrderByDescending(n => n.Date);
        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }
}
