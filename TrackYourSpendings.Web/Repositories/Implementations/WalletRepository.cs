using TrackYourSpendings.Web.Context;
using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Repositories.Implementations;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    public WalletRepository(DataContext dataContext) : base(dataContext)
    {
    }
}