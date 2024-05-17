using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Queries;

public class GetWalletRequest : IRequest<WalletDto>
{
    public Guid WalletId { get; set; }
    public Guid UserId { get; set; }
}