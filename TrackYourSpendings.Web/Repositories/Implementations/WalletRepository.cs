using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Web.Context;
using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Repositories.Implementations;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    public WalletRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<Wallet?> GetWalletDetails(Expression<Func<Wallet, bool>> predicate)
    {
        var wallet = await DbEntitySet.Where(predicate)
            .Include(wal => wal.Transactions)!
            .ThenInclude(tr => tr.Category)
            .FirstOrDefaultAsync();

        return wallet;
    }
}