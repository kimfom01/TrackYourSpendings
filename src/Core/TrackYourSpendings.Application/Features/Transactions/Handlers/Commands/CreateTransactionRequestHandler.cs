using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Dtos.Transactions;
using TrackYourSpendings.Application.Exceptions;
using TrackYourSpendings.Application.Features.Transactions.Requests.Commands;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Application.Features.Transactions.Handlers.Commands;

public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequest, GetTransactionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTransactionRequestHandler> _logger;

    public CreateTransactionRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CreateTransactionRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetTransactionDto> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding new transaction");
        var wallet =
            await _unitOfWork.Wallets.GetActiveWallet(request.UserId);

        if (wallet is null)
        {
            _logger.LogError("No active wallet to add transactions to");
            throw new NotFoundException("No active wallet to add transactions to");
        }

        var transaction = _mapper.Map<Transaction>(request.TransactionDto);

        transaction.Date = DateTime.Now;
        transaction.WalletId = wallet.Id;
        transaction.UserId = request.UserId;

        var added = await _unitOfWork.Transactions.AddEntity(transaction);

        wallet.Expenses += transaction.Cost;
        wallet.Balance -= transaction.Cost;

        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Transaction successfully added");

        return _mapper.Map<GetTransactionDto>(added);
    }
}