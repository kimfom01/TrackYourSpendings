using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Queries;

public class GetActiveWalletRequestHandler : IRequestHandler<GetActiveWalletRequest, WalletDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetActiveWalletRequestHandler> _logger;
    private readonly IMapper _mapper;

    public GetActiveWalletRequestHandler(
        ILogger<GetActiveWalletRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<WalletDto?> Handle(GetActiveWalletRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting active wallet for user={userId}", request.UserId);
        var wallet = await _unitOfWork.Wallets.GetEntity(wall =>
            wall.Active && wall.UserId == request.UserId);

        if (wallet is null)
        {
            _logger.LogError("No wallet has been set active yet for user={userId}", request.UserId);
            // throw new NotFoundException($"No wallet has been set active yet for user={request.UserId}");
        }

        return _mapper.Map<WalletDto>(wallet);
    }
}