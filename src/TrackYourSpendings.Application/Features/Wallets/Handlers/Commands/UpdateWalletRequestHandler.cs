using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Commands;

public class UpdateWalletRequestHandler : IRequestHandler<UpdateWalletRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateWalletRequestHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateWalletRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<UpdateWalletRequestHandler> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateWalletRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating wallet={walletId}", request.WalletId);
        var wallet = await _unitOfWork.Wallets
            .GetEntity(wal => wal.Id == request.WalletId && wal.UserId == request.UserId);

        if (wallet is not null)
        {
            if (wallet.Balance == 0)
            {
                wallet.Balance = request.WalletUpdateDto!.Income;
            }
            else
            {
                var incomeDifference = request.WalletUpdateDto!.Income - wallet.Income;

                if (incomeDifference != 0)
                {
                    wallet.Balance += incomeDifference;
                }
            }

            _mapper.Map(request.WalletUpdateDto, wallet);

            await _unitOfWork.SaveChanges(cancellationToken);
        }
        else
        {
            _logger.LogError("Wallet with id={walletId} does not exist for user={userId}", request.WalletId,
                request.UserId);
            // throw new NotFoundException($"Wallet with id={request.WalletId} does not exist for user={request.UserId}");    
        }

        _logger.LogInformation("Successfully updated wallet={walletId}", request.WalletId);
        return Unit.Value;
    }
}