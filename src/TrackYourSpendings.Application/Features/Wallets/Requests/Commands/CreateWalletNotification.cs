using MediatR;

namespace TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

public class CreateWalletNotification : INotification
{
    public string? UserId { get; set; }
}