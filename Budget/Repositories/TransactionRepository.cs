using Budget.Data;
using Budget.Models;

namespace Budget.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(BudgetDbContext budgetDbContext) : base(budgetDbContext)
    {
    }
}