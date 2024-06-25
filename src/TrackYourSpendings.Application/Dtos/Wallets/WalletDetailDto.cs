using TrackYourSpendings.Application.Dtos.Common;
using TrackYourSpendings.Application.Dtos.Transactions;

namespace TrackYourSpendings.Application.Dtos.Wallets;

public class WalletDetailDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public decimal Income { get; set; }

    public decimal Expenses { get; set; }

    public decimal Balance { get; set; }

    public bool Active { get; set; }

    public IEnumerable<CreateTransactionDto>? Transactions { get; set; }

    public string? UserId { get; set; }
}