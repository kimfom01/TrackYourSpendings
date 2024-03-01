using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Services;

/// <summary>
/// Defines the contract for services managing <see cref="Wallet"/> entities.
/// </summary>
/// <remarks>
/// This interface abstracts the business logic associated with wallet management, facilitating operations such as the creation, update, and deletion of wallets, 
/// as well as managing the active wallet state for users. It promotes a separation of concerns between the application's data access layer and its business logic.
/// </remarks>
public interface IWalletService
{
    /// <summary>
    /// Adds a new wallet for a given user.
    /// </summary>
    /// <param name="wallet">The wallet to be added.</param>
    /// <param name="userId">The identifier of the user who owns the wallet.</param>
    /// <returns>A task representing the asynchronous operation, containing the added wallet.</returns>
    Task<Wallet?> AddWallet(Wallet wallet, string? userId);

    /// <summary>
    /// Updates an existing wallet with new information.
    /// </summary>
    /// <param name="wallet">The wallet containing updated information.</param>
    /// <param name="userId">The identifier of the user who owns the wallet.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    Task UpdateWallet(Wallet wallet, string? userId);

    /// <summary>
    /// Deletes a specified wallet for a given user.
    /// </summary>
    /// <param name="wallet">The wallet to be deleted.</param>
    /// <param name="userId">The identifier of the user who owns the wallet.</param>
    /// <returns>A task representing the asynchronous deletion operation.</returns>
    Task DeleteWallet(Wallet wallet, string? userId);

    /// <summary>
    /// Retrieves the active wallet for a given user.
    /// </summary>
    /// <param name="userId">The identifier of the user whose active wallet is to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, containing the active wallet if one exists; otherwise, null.</returns>
    Task<Wallet?> GetActiveWallet(string? userId);

    /// <summary>
    /// Sets a specified wallet as the active wallet for a given user.
    /// </summary>
    /// <param name="wallet">The wallet to be set as active.</param>
    /// <param name="userId">The identifier of the user for whom the wallet will be set as active.</param>
    /// <returns>A task representing the asynchronous operation to set the wallet as active.</returns>
    Task SetActiveWallet(Wallet wallet, string? userId);

    /// <summary>
    /// Retrieves all inactive wallets for a given user.
    /// </summary>
    /// <param name="userId">The identifier of the user whose inactive wallets are to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, containing an enumerable of inactive wallets.</returns>
    Task<IEnumerable<Wallet>> GetInactiveWallets(string? userId);

    /// <summary>
    /// Retrieves a specific wallet by its identifier for a given user.
    /// </summary>
    /// <param name="walletId">The identifier of the wallet to be retrieved.</param>
    /// <param name="userId">The identifier of the user who owns the wallet.</param>
    /// <returns>A task representing the asynchronous operation, containing the wallet if found; otherwise, null.</returns>
    Task<Wallet?> GetWallet(int? walletId, string? userId);
}
