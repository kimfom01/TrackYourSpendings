using TrackYourSpendings.Application.Dtos.Common;
using TrackYourSpendings.Domain.Entities;
using TrackYourSpendings.Domain.Enums;

namespace TrackYourSpendings.Application.Dtos.Transactions;

public class GetTransactionDto : BaseDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Month? Month { get; set; }
    public decimal Cost { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid WalletId { get; set; }
}