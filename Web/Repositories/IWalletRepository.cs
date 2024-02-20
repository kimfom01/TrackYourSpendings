using Web.Models;

namespace Web.Repositories;

/// <summary>
/// Defines the contract for a repository specifically managing <see cref="Wallet"/> entities, extending the common operations provided by the <see cref="IRepository{TEntity}"/>.
/// </summary>
/// <remarks>
/// This interface may be extended in the future to include operations specific to wallets, such as retrieving wallet balances or transactions by wallet.
/// It currently serves as a marker for dependency injection and to facilitate mocking in unit tests.
/// </remarks>
public interface IWalletRepository : IRepository<Wallet>
{
}