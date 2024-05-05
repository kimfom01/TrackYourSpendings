using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

public class GetWalletDetailsRequest : IRequest<WalletDetailDto>
{
    public Guid UserId { get; set; }
    public Guid WalletId { get; set; }
}