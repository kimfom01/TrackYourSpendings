using Web.Dtos;
using Web.Models;

namespace Web.Services;

public interface ITransactionService
{
    Task<Transaction> AddTransaction(Transaction transaction, string? userId);
    Task UpdateTransaction(TransactionDto transaction, string? userId);
    Task DeleteTransaction(Transaction transaction, string? userId);

    Task<IEnumerable<TransactionDto>> SearchAndFilter(string? userId, int? walletId, string? searchString,
        int? category, DateTime? date);

    Task<IEnumerable<TransactionDto>> GetTransactions(string? userId);
}