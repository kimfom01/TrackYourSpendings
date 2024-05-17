using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

public class GetInactiveWalletsRequest : IRequest<List<WalletDto>>
{
    public Guid UserId { get; set; }
}