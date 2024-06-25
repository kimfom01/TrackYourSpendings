using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Contracts.Database.Repository;
using TrackYourSpendings.Infrastructure.Database.Repositories;

namespace TrackYourSpendings.Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDataContext _dbContext;

    public UnitOfWork(AppDataContext dbContext)
    {
        _dbContext = dbContext;
        Categories = new CategoryRepository(dbContext);
        Transactions = new TransactionRepository(dbContext);
        Wallets = new WalletRepository(dbContext);
    }

    public ICategoryRepository Categories { get; }
    public ITransactionRepository Transactions { get; }
    public IWalletRepository Wallets { get; }

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}