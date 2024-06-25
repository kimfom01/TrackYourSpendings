using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Queries;

public class GetInactiveWalletsRequestHandler : IRequestHandler<GetInactiveWalletsRequest, List<WalletDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetInactiveWalletsRequestHandler> _logger;
    private readonly IMapper _mapper;

    public GetInactiveWalletsRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetInactiveWalletsRequestHandler> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<WalletDto>> Handle(GetInactiveWalletsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wallets for user={userId}", request.UserId);
        var wallets = await _unitOfWork.Wallets
            .GetEntities(wal => wal.UserId == request.UserId && !wal.Active);

        if (wallets is null)
        {
            return [];
        }

        return _mapper.Map<List<WalletDto>>(wallets);
    }
}