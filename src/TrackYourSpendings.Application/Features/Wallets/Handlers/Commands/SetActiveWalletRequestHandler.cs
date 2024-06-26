using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Commands;

public class SetActiveWalletRequestHandler : IRequestHandler<SetActiveWalletRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetActiveWalletRequestHandler> _logger;

    public SetActiveWalletRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<SetActiveWalletRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(SetActiveWalletRequest request, CancellationToken cancellationToken)
    {
        // validate request before processing

        _logger.LogInformation("Setting wallet={walletId} active", request.WalletId);
        var currentActiveWallet =
            await _unitOfWork.Wallets.GetActiveWallet(request.UserId!);

        var wallet = await _unitOfWork.Wallets
            .GetEntity(wal => wal.UserId == request.UserId && wal.Id == request.WalletId);

        if (currentActiveWallet is not null)
        {
            currentActiveWallet.Active = false;
        }

        if (wallet is not null)
        {
            wallet.Active = true;
            await _unitOfWork.SaveChanges(cancellationToken);
        }
        else
        {
            _logger.LogError("Wallet with id={walletId} does not exist for user={userId}", request.WalletId,
                request.UserId);
            // throw new NotFoundException($"Wallet with id={request.WalletId} does not exist for user={request.UserId}");
        }

        return Unit.Value;
    }
}