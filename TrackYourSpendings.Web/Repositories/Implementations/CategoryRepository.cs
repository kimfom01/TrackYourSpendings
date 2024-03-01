using TrackYourSpendings.Web.Context;
using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Repositories.Implementations;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext dataContext) : base(dataContext)
    {
    }
}