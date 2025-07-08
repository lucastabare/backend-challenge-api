using NewsApi.ViewModels;
using NewsApi.ViewModels.Filters;

namespace NewsApi.Services.Interfaces;

public interface IPaginatedService<TModel, TViewModel>
{
    Task<PaginationViewModel<TViewModel>> GetPaginatedAsync(PaginationFilterViewModel filter);
}
