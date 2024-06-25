using MediatR;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Commands;

namespace TrackYourSpendings.Application.Features.Wallets.Handlers.Commands;

public class CreateWalletNotificationHandler : INotificationHandler<CreateWalletNotification>
{
    private readonly IMediator _mediator;

    public CreateWalletNotificationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(CreateWalletNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new WalletCreateRequest
            {
                UserId = notification.UserId,
                WalletCreateDto = new WalletCreateDto
                {
                    Name = DateTime.Now.ToString("MMMM"),
                    Currency = "USD",
                    Income = 0M,
                }
            },
            cancellationToken);
    }
}