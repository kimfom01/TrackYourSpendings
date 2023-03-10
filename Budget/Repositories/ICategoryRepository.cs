using Budget.Models;

namespace Budget.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public IEnumerable<Category> GetCategoriesWithTransactions();
}