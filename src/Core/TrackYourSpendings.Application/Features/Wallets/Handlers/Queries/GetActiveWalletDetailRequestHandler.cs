using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Queries;

public class GetActiveWalletDetailRequestHandler : IRequestHandler<GetActiveWalletDetailRequest, WalletDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetActiveWalletDetailRequestHandler> _logger;
    private readonly IMapper _mapper;

    public GetActiveWalletDetailRequestHandler(
        ILogger<GetActiveWalletDetailRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<WalletDetailDto> Handle(GetActiveWalletDetailRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting active wallet details for user={userId}", request.UserId);
        var wallet = await _unitOfWork.Wallets.GetActiveWalletDetails(request.UserId!);

        if (wallet is null)
        {
            _logger.LogError("No wallet has been set active yet for user={userId}", request.UserId);
            // throw new NotFoundException($"No wallet has been set active yet for user={request.UserId}");
        }

        return _mapper.Map<WalletDetailDto>(wallet);
    }
}