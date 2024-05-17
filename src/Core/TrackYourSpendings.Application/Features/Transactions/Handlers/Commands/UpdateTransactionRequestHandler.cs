using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Exceptions;
using TrackYourSpendings.Application.Features.Transactions.Requests.Commands;

namespace TrackYourSpendings.Application.Features.Transactions.Handlers.Commands;

public class UpdateTransactionRequestHandler : IRequestHandler<UpdateTransactionRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateTransactionRequestHandler> _logger;

    public UpdateTransactionRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<UpdateTransactionRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating transaction with id={transactionId}", request.TransactionDto.Id);

        var transaction = await _unitOfWork.Transactions.GetEntity(tr =>
            tr.Id == request.TransactionDto.Id && tr.UserId == request.UserId);

        if (transaction is null)
        {
            _logger.LogError("Transaction with id={transactionId} does not exist", request.TransactionDto.Id);
            throw new NotFoundException("Transaction does not exist");
        }

        var wallet = await _unitOfWork.Wallets.GetEntity(wal =>
            wal.Id == transaction.WalletId && wal.UserId == request.UserId);

        if (wallet is null)
        {
            _logger.LogError("Wallet with id={walletId} does not exist", request.TransactionDto.WalletId);
            throw new NotFoundException("Wallet does not exist");
        }

        if (request.TransactionDto.Cost >= 0)
        {
            var costDifference = transaction.Cost - request.TransactionDto.Cost;

            if (costDifference != 0)
            {
                wallet.Expenses -= costDifference;
                wallet.Balance += costDifference;
            }
        }

        transaction = _mapper.Map(request.TransactionDto, transaction);

        await _unitOfWork.Transactions.Update(transaction);
        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Transaction successfully updated");

        return Unit.Value;
    }
}