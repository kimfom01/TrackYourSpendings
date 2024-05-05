using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

public class UpdateWalletRequest : IRequest<Unit>
{
    public WalletUpdateDto WalletUpdateDto { get; set; }
    public Guid UserId { get; set; }
    public Guid WalletId { get; set; }
}