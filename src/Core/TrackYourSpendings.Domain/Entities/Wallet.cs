using System.ComponentModel.DataAnnotations;
using TrackYourSpendings.Domain.Entities.Common;

namespace TrackYourSpendings.Domain.Entities;

public class Wallet : BaseEntity
{
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
    [MaxLength(5)] public string Currency { get; set; } = string.Empty;
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public decimal Balance { get; set; }
    public bool Active { get; set; }

    public IEnumerable<Transaction>? Transactions { get; set; }
}