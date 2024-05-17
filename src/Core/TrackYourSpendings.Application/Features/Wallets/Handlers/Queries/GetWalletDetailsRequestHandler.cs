using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Exceptions;
using TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Queries;

public class GetWalletDetailsRequestHandler : IRequestHandler<GetWalletDetailsRequest, WalletDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetWalletDetailsRequestHandler> _logger;
    private readonly IMapper _mapper;

    public GetWalletDetailsRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetWalletDetailsRequestHandler> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<WalletDetailDto> Handle(GetWalletDetailsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wallet details for user={userId}", request.UserId);
        var wallet = await _unitOfWork.Wallets.GetWalletDetails(request.WalletId, request.UserId);

        if (wallet is null)
        {
            _logger.LogError("Wallet with id={walletId} does not exist for user={userId}", request.WalletId,
                request.UserId);
            throw new NotFoundException($"Wallet with id={request.WalletId} does not exist for user={request.UserId}");
        }

        return _mapper.Map<WalletDetailDto>(wallet);
    }
}