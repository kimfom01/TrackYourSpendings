using MediatR;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

public class SetActiveWalletRequest : IRequest<Unit>
{
    public Guid WalletId { get; set; }
    public Guid UserId { get; set; }
}