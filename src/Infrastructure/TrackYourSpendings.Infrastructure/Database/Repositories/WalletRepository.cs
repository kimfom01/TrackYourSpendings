using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Application.Contracts.Persistence.Repository;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Infrastructure.Database.Repositories;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    public WalletRepository(AppDataContext appDataContext) : base(appDataContext)
    {
    }

    public async Task<Wallet?> GetWalletDetails(Guid walletId, string userId)
    {
        var wallet = await DbEntitySet.Where(wall => wall.Id == walletId && wall.UserId == userId)
            .Include(wal => wal.Transactions)!
            .ThenInclude(tr => tr.Category)
            .FirstOrDefaultAsync();

        return wallet;
    }

    public async Task<Wallet?> GetActiveWallet(string userId)
    {
        var wallet = await DbEntitySet.FirstOrDefaultAsync(wal => wal.Active && wal.UserId == userId);

        return wallet;
    }

    public async Task<Wallet?> GetActiveWalletDetails(string userId)
    {
        var wallet = await DbEntitySet.Where(wal => wal.Active && wal.UserId == userId)
            .Include(wal => wal.Transactions)!
            .ThenInclude(tr => tr.Category)
            .FirstOrDefaultAsync();

        return wallet;
    }
}