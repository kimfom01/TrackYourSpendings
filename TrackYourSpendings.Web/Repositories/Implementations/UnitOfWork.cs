using TrackYourSpendings.Web.Context;

namespace TrackYourSpendings.Web.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext)
    {
        _dbContext = dbContext;
        Categories = new CategoryRepository(dbContext);
        Transactions = new TransactionRepository(dbContext);
        Wallets = new WalletRepository(dbContext);
    }

    public ICategoryRepository Categories { get; }
    public ITransactionRepository Transactions { get; }
    public IWalletRepository Wallets { get; }

    public async Task<int> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}