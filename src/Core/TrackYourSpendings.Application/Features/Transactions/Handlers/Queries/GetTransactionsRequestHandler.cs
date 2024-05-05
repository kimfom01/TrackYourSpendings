using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Dtos.Transactions;
using TrackYourSpendings.Application.Exceptions;
using TrackYourSpendings.Application.Features.Transactions.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Transactions.Handlers.Queries;

public class GetTransactionsRequestHandler : IRequestHandler<GetTransactionsRequest, List<GetTransactionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetTransactionsRequestHandler> _logger;

    public GetTransactionsRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetTransactionsRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetTransactionDto>> Handle(GetTransactionsRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting transaction for user with id={userId}", request.UserId);

        var wallet = await _unitOfWork.Wallets.GetActiveWallet(request.UserId);

        if (wallet is null)
        {
            _logger.LogError("No active wallet for user with id={userId}", request.UserId);
            throw new NotFoundException("No active wallet");
        }

        var transactions = await _unitOfWork.Transactions.GetTransactionsWithCategories(tr =>
            tr.WalletId == wallet.Id && tr.UserId == request.UserId);

        return _mapper.Map<List<GetTransactionDto>>(transactions);
    }
}