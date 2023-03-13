using Budget.Models;

namespace Budget.Repositories;

public interface IWalletRepository : IRepository<Wallet>
{
    public IEnumerable<Wallet> GetWalletWithTransactions();
}