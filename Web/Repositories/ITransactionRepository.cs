using System.Linq.Expressions;
using Web.Models;

namespace Web.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    public Task<IEnumerable<Transaction>?> GetTransactionsWithCategories(Expression<Func<Transaction, bool>> predicate);
}