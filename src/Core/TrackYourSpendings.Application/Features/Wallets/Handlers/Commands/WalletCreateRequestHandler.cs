using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Commands;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Commands;

public class WalletCreateRequestHandler : IRequestHandler<WalletCreateRequest, WalletDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WalletCreateRequestHandler> _logger;
    private readonly IMapper _mapper;

    public WalletCreateRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<WalletCreateRequestHandler> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<WalletDto> Handle(WalletCreateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new wallet for user={userId}", request.UserId);
        var wallet = _mapper.Map<Wallet>(request.WalletCreateDto);
        wallet.Balance = request.WalletCreateDto.Income;
        wallet.UserId = request.UserId;

        wallet = await _unitOfWork.Wallets.AddEntity(wallet);
        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Created new wallet for user={userId}", request.UserId);

        return _mapper.Map<WalletDto>(wallet);
    }
}