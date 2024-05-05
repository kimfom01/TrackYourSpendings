using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

public class GetActiveWalletDetailRequest : IRequest<WalletDetailDto>
{
    public Guid UserId { get; set; }
}