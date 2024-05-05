using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Exceptions;
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
        _logger.LogInformation("Setting wallet={walletId} active", request.WalletId);
        var currentActiveWallet =
            await _unitOfWork.Wallets.GetActiveWallet(request.UserId);

        var wallet = await _unitOfWork.Wallets
            .GetEntity(wal => wal.UserId == request.UserId && wal.Id == request.WalletId);

        if (currentActiveWallet is not null)
        {
            currentActiveWallet.Active = false;
        }

        if (wallet is null)
        {
            throw new NotFoundException($"Wallet with id={request.WalletId} does not exist for user={request.UserId}");
        }

        wallet.Active = true;
        await _unitOfWork.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}