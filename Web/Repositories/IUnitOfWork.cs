namespace Web.Repositories;

/// <summary>
/// Defines the contract for a UnitOfWork, grouping all changes to be committed to the database in a single transaction.
/// </summary>
/// <remarks>
/// This interface ensures the implementation of the Unit of Work pattern, facilitating the coordination of work across multiple repositories.
/// It also extends <see cref="IDisposable"/> to ensure proper cleanup of resources.
/// </remarks>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the category repository for managing category entities.
    /// </summary>
    /// <value>
    /// The category repository.
    /// </value>
    public ICategoryRepository Categories { get; }
    
    /// <summary>
    /// Gets the transaction repository for managing transaction entities.
    /// </summary>
    /// <value>
    /// The transaction repository.
    /// </value>
    public ITransactionRepository Transactions { get; }
    
    /// <summary>
    /// Gets the wallet repository for managing wallet entities.
    /// </summary>
    /// <value>
    /// The wallet repository.
    /// </value>
    public IWalletRepository Wallets { get; }

    /// <summary>
    /// Commits all changes made within the unit of work to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    public Task<int> SaveChanges();
}