using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Queries;

public class GetWalletRequestHandler : IRequestHandler<GetWalletRequest, WalletDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetWalletRequestHandler> _logger;
    private readonly IMapper _mapper;

    public GetWalletRequestHandler(
        IMapper mapper,
        ILogger<GetWalletRequestHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<WalletDto?> Handle(GetWalletRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wallet details for user={userId}", request.UserId);
        var wallet =
            await _unitOfWork.Wallets.GetEntity(wal => wal.Id == request.WalletId && wal.UserId == request.UserId);

        if (wallet is null)
        {
            _logger.LogError("Wallet with id={walletId} does not exist for user={userId}", request.WalletId,
                request.UserId);
            // throw new NotFoundException($"Wallet with id={request.WalletId} does not exist for user={request.UserId}");
        }

        return _mapper.Map<WalletDto>(wallet);
    }
}