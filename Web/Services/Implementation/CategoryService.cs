using Web.Models;
using Web.Repositories;

namespace Web.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        var categories = await _unitOfWork.Categories.GetEntities(_ => true);

        return categories ?? Enumerable.Empty<Category>();
    }
}