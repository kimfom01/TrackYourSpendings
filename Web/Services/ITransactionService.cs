using Web.Dtos;
using Web.Models;

namespace Web.Services;

/// <summary>
/// Defines the contract for services managing <see cref="Transaction"/> entities.
/// </summary>
/// <remarks>
/// This interface abstracts the business logic associated with transaction management, including creation, modification, deletion, and retrieval of transaction records.
/// It serves to decouple the application's core business logic from direct data access operations, promoting a clean architecture.
/// </remarks>
public interface ITransactionService
{
    /// <summary>
    /// Adds a new transaction associated with a given user.
    /// </summary>
    /// <param name="transaction">The transaction to add.</param>
    /// <param name="userId">The user's identifier to whom the transaction belongs.</param>
    /// <returns>A task representing the asynchronous operation, containing the added transaction.</returns>
    Task<Transaction> AddTransaction(Transaction transaction, string? userId);

    /// <summary>
    /// Updates an existing transaction based on provided data transfer object (DTO).
    /// </summary>
    /// <param name="transaction">The DTO containing updated transaction information.</param>
    /// <param name="userId">The user's identifier associated with the transaction.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    Task UpdateTransaction(TransactionDto transaction, string? userId);

    /// <summary>
    /// Deletes a specified transaction for a given user.
    /// </summary>
    /// <param name="transaction">The transaction to delete.</param>
    /// <param name="userId">The user's identifier associated with the transaction to be deleted.</param>
    /// <returns>A task representing the asynchronous deletion operation.</returns>
    Task DeleteTransaction(Transaction transaction, string? userId);

    /// <summary>
    /// Searches and filters transactions based on specified criteria such as wallet ID, search string, category, and date.
    /// </summary>
    /// <param name="userId">The user's identifier for whom to filter transactions.</param>
    /// <param name="walletId">Optional wallet identifier to narrow down transactions.</param>
    /// <param name="searchString">Optional search string to filter transactions by description or other text fields.</param>
    /// <param name="category">Optional category ID to filter transactions.</param>
    /// <param name="date">Optional date to filter transactions by a specific day.</param>
    /// <returns>A task representing the asynchronous operation, containing an enumerable of transactions that match the criteria.</returns>
    Task<IEnumerable<TransactionDto>> SearchAndFilter(string? userId, int? walletId, string? searchString, int? category, DateTime? date);

    /// <summary>
    /// Retrieves all transactions associated with a given user.
    /// </summary>
    /// <param name="userId">The user's identifier whose transactions are to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, containing an enumerable of all transactions associated with the specified user.</returns>
    Task<IEnumerable<TransactionDto>> GetTransactions(string? userId);
}
