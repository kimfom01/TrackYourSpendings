using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Web.Context;
using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Repositories.Implementations;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<Transaction>?> GetTransactionsWithCategories(Expression<Func<Transaction, bool>> predicate)
    {
        var transactionsWithCategories = DbEntitySet
            .Where(predicate)
            .Include(tr => tr.Category);

        return await transactionsWithCategories.ToListAsync();
    }

    public override async Task<Transaction?> GetEntity(Expression<Func<Transaction, bool>> predicate)
    {
        var entity = await DbEntitySet.AsNoTracking().FirstOrDefaultAsync(predicate);

        return entity;
    }
}