using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<News> News => Set<News>();
}
