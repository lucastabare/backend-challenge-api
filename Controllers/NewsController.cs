using Microsoft.AspNetCore.Mvc;
using NewsApi.Controllers.Base;
using NewsApi.Models;
using NewsApi.Services.Interfaces;
using NewsApi.ViewModels;

namespace NewsApi.Controllers;

[Route("api/[controller]")]
public class NewsController : PaginatedController<News, NewsViewModel>
{
    private readonly INewsService _service;

    public NewsController(INewsService service, IPaginatedService<News, NewsViewModel> paginatedService)
        : base(paginatedService)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var news = await _service.GetByIdAsync(id);
        return news is null ? NotFound() : Ok(news);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewsPostViewModel model)
    {
        var created = await _service.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] NewsPutViewModel model)
    {
        var success = await _service.UpdateAsync(id, model);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? query) =>
        Ok(await _service.SearchAsync(query));
}
