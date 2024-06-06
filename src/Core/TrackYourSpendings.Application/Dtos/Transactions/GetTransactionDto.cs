using TrackYourSpendings.Application.Dtos.Common;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Application.Dtos.Transactions;

public class GetTransactionDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid WalletId { get; set; }
}