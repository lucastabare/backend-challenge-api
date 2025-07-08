using Microsoft.AspNetCore.Mvc;
using NewsApi.Services.Interfaces;
using NewsApi.ViewModels;
using NewsApi.ViewModels.Filters; 

namespace NewsApi.Controllers.Base;

[ApiController]
public abstract class PaginatedController<TModel, TViewModel> : ControllerBase
{
    private readonly IPaginatedService<TModel, TViewModel> _service;

    protected PaginatedController(IPaginatedService<TModel, TViewModel> service)
    {
        _service = service;
    }

    [HttpGet]
    public virtual async Task<IActionResult> Get([FromQuery] PaginationFilterViewModel filter)
    {
        var result = await _service.GetPaginatedAsync(filter);
        return Ok(result);
    }
}
