using TrackYourSpendings.Application.Contracts.Database.Repository;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Infrastructure.Database.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDataContext appDataContext) : base(appDataContext)
    {
    }
}