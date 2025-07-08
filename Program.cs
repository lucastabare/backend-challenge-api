using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Repositories;
using NewsApi.Repositories.Interfaces;
using NewsApi.Services;
using NewsApi.Services.Interfaces;
using NewsApi.ViewModels;
using NewsApi.ViewModels.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IPaginatedService<News, NewsViewModel>, NewsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("*") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowLocalhost3000");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
