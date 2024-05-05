using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Features.Transactions.Requests.Commands;

namespace TrackYourSpendings.Application.Features.Transactions.Handlers.Commands;

public class DeleteTransactionRequestHandler : IRequestHandler<DeleteTransactionRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteTransactionRequestHandler> _logger;

    public DeleteTransactionRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteTransactionRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting transaction with id={transactionId}", request.TransactionDto.Id);

        await _unitOfWork.Transactions.RemoveEntity(tra =>
            tra.Id == request.TransactionDto.Id && tra.UserId == request.UserId);

        var wallet = await _unitOfWork.Wallets.GetEntity(wal =>
            wal.Id == request.TransactionDto.WalletId && wal.UserId == request.UserId);
        
        if (wallet is not null)
        {
            wallet.Expenses -= request.TransactionDto.Cost;
            wallet.Balance += request.TransactionDto.Cost;
        }

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Transaction successfully deleted");
        return Unit.Value;
    }
}