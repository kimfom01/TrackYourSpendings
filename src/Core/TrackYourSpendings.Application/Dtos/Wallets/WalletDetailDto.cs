using TrackYourSpendings.Application.Dtos.Common;
using TrackYourSpendings.Application.Dtos.Transactions;

namespace TrackYourSpendings.Application.Dtos.Wallets;

public class WalletDetailDto : BaseDto
{
    public string Name { get; set; }

    public string Currency { get; set; }

    public decimal Income { get; set; }

    public decimal Expenses { get; set; }

    public decimal Balance { get; set; }

    public bool Active { get; set; }

    public IEnumerable<CreateTransactionDto>? Transactions { get; set; }

    public string? UserId { get; set; }
}