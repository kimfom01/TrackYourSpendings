namespace Budget.Repositories;

public interface IUnitOfWork
{
    public ICategoryRepository Categories { get; }
    public ITransactionRepository Transactions { get; }
    public IWalletRepository Wallets { get; }

    public Task<int> SaveChanges();
}