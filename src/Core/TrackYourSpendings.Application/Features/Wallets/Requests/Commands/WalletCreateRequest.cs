using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

public class WalletCreateRequest : IRequest<WalletDto>
{
    public WalletCreateDto? WalletCreateDto { get; set; }
    public string? UserId { get; set; }
}