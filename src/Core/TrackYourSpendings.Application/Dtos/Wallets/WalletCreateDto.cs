namespace TrackYourSpendings.Application.Dtos.Wallets;

public class WalletCreateDto
{
    public string Name { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public decimal Income { get; set; }
}