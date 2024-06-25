using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

public class UpdateWalletRequest : IRequest<Unit>
{
    public WalletUpdateDto? WalletUpdateDto { get; set; }
    public string? UserId { get; set; }
    public Guid WalletId { get; set; }
}