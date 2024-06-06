using TrackYourSpendings.Application.Dtos.Common;

namespace TrackYourSpendings.Application.Dtos.Wallets;

public class WalletUpdateDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Income { get; set; }
}