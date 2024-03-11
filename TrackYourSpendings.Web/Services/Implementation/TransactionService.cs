using AutoMapper;
using TrackYourSpendings.Web.Dtos;
using TrackYourSpendings.Web.Models;
using TrackYourSpendings.Web.Repositories;

namespace TrackYourSpendings.Web.Services.Implementation;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;

    public TransactionService(
        IUnitOfWork unitOfWork,
        IWalletService walletService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _walletService = walletService;
        _mapper = mapper;
    }

    public async Task<Transaction> AddTransaction(Transaction transaction, string? userId)
    {
        var wallet = await _walletService.GetActiveWallet(userId);

        if (wallet is null)
        {
            throw new NullReferenceException("No active wallet");
        }

        transaction.Date = DateTime.Now;
        transaction.WalletId = wallet.Id;
        transaction.UserId = userId;

        var added = await _unitOfWork.Transactions.AddEntity(transaction);

        wallet.Expenses += transaction.Cost;
        wallet.Balance -= transaction.Cost;

        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task UpdateTransaction(TransactionDto transaction, string? userId)
    {
        var oldTransaction = await _unitOfWork.Transactions.GetEntity(tr =>
            tr.Id == transaction.Id && tr.UserId == userId);

        if (oldTransaction is null)
        {
            throw new NullReferenceException("Transaction does not exist");
        }

        var wallet = await _walletService.GetWallet(oldTransaction.WalletId, userId);

        if (wallet is null)
        {
            throw new NullReferenceException("Wallet does not exist");
        }

        if (transaction.Cost is not null && oldTransaction.Cost is not null)
        {
            var costDifference = oldTransaction.Cost.Value - transaction.Cost.Value;

            if (costDifference != 0)
            {
                wallet.Expenses -= costDifference;
                wallet.Balance += costDifference;
            }
        }

        oldTransaction = _mapper.Map(transaction, oldTransaction);

        await _unitOfWork.Transactions.Update(oldTransaction);
        await _unitOfWork.SaveChanges();
    }

    public async Task DeleteTransaction(Transaction transaction, string? userId)
    {
        await _unitOfWork.Transactions.RemoveEntity(tr =>
            tr.Id == transaction.Id && tr.UserId == userId);

        var wallet = await _walletService.GetWallet(transaction.WalletId, userId);

        if (wallet is not null)
        {
            wallet.Expenses -= transaction.Cost;
            wallet.Balance += transaction.Cost;

            await _unitOfWork.SaveChanges();
        }
    }

    public async Task<IEnumerable<TransactionDto>> SearchAndFilter(string? userId, int? walletId, string? searchString,
        int? category, DateTime? date)
    {
        var transactions = await _unitOfWork.Transactions
            .GetTransactionsWithCategories(tr =>
                tr.WalletId == walletId && tr.UserId == userId);

        if (transactions is null)
        {
            return Enumerable.Empty<TransactionDto>();
        }

        if (searchString is not null)
        {
            transactions = transactions.Where(tr =>
                tr.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
        }

        if (category is not null)
        {
            transactions = transactions.Where(tr => tr.CategoryId == category);
        }

        if (date is not null)
        {
            transactions = transactions.Where(tr => tr.Date?.Date == date.Value.Date);
        }

        var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactions);

        return transactionDtos;
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactions(string? userId)
    {
        var wallet = await _walletService.GetActiveWallet(userId);

        if (wallet is null)
        {
            throw new NullReferenceException("No active wallet");
        }

        var transactions =
            await _unitOfWork.Transactions.GetTransactionsWithCategories(tr =>
                tr.WalletId == wallet.Id && tr.UserId == userId && tr.Date!.Value.Month == DateTime.Now.Month);

        var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactions);

        return transactionDtos ?? Enumerable.Empty<TransactionDto>();
    }
}