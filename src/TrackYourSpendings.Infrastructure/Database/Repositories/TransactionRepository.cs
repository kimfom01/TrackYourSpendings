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

    public async Task<IEnumerable<Transaction>?> GetTransactionsWithCategories(Guid? walletId, string? userId)
    {
        if (walletId is null || userId is null)
        {
            return [];
        }

        var transactionsWithCategories = DbEntitySet
            .Where(tr =>
                tr.WalletId == walletId && tr.UserId == userId)
            .Include(tr => tr.Category);

        return await transactionsWithCategories.ToListAsync();
    }

    public override async Task<Transaction?> GetEntity(Expression<Func<Transaction, bool>> predicate)
    {
        var entity = await DbEntitySet.AsNoTracking().FirstOrDefaultAsync(predicate);

        return entity;
    }
}