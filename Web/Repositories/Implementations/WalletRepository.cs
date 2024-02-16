using Web.Context;
using Web.Models;

namespace Web.Repositories.Implementations;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    public WalletRepository(DataContext dataContext) : base(dataContext)
    {
    }
}