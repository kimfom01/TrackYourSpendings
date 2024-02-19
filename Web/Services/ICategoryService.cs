using Web.Models;

namespace Web.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategories();
}