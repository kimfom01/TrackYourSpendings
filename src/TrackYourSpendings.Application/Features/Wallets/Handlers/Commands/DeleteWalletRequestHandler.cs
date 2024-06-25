using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Commands;

public class DeleteWalletRequestHandler : IRequestHandler<DeleteWalletRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteWalletRequestHandler> _logger;

    public DeleteWalletRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteWalletRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWalletRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting wallet={walletId}", request.WalletId);
        await _unitOfWork.Wallets.RemoveEntity(wal => wal.Id == request.WalletId && wal.UserId == request.UserId);

        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully deleted wallet={walletId}", request.WalletId);

        return Unit.Value;
    }
}