namespace TrackYourSpendings.Application.Dtos.Wallets;

public class WalletCreateDto
{
    public string Name { get; set; }

    public string Currency { get; set; }

    public decimal Income { get; set; }
}