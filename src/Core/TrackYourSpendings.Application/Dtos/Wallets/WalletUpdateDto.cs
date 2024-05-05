using TrackYourSpendings.Application.Dtos.Common;

namespace TrackYourSpendings.Application.Dtos.Wallets;

public class WalletUpdateDto : BaseDto
{
    public string Name { get; set; }
    public string Currency { get; set; }
    public decimal Income { get; set; }
}