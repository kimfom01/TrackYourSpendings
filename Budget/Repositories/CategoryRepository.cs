using Budget.Context;
using Budget.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(BudgetDbContext budgetDbContext) : base(budgetDbContext)
    {
    }

    public IEnumerable<Category> GetCategoriesWithTransactions()
    {
        return DbEntitySet.Include(cat => cat.Transactions);
    }
}