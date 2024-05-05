using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Application.Contracts.Persistence.Repository;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Infrastructure.Database.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDataContext appDataContext) : base(appDataContext)
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