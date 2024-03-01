using System.Linq.Expressions;
using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Repositories;

/// <summary>
/// Defines the contract for a repository managing <see cref="Transaction"/> entities, extending the generic <see cref="IRepository{TEntity}"/> interface.
/// </summary>
/// <remarks>
/// Adds specialized behavior for retrieving transactions along with their associated categories, facilitating queries that require transaction details
/// and related category information in a single operation.
/// </remarks>
public interface ITransactionRepository : IRepository<Transaction>
{
    /// <summary>
    /// Retrieves all transactions that satisfy the specified predicate, including their associated category details.
    /// </summary>
    /// <param name="predicate">A function to test each transaction for a condition, used to filter the transactions included in the result.</param>
    /// <returns>
    /// A task that represents the asynchronous operation, containing the list of transactions with their categories if found; otherwise, null.
    /// </returns>
    /// <remarks>
    /// This method is particularly useful for operations requiring access to both transaction and category information, optimizing data retrieval
    /// by minimizing the number of separate queries needed.
    /// </remarks>
    Task<IEnumerable<Transaction>?> GetTransactionsWithCategories(Expression<Func<Transaction, bool>> predicate);
}
