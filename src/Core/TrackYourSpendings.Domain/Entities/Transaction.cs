using System.ComponentModel.DataAnnotations;
using TrackYourSpendings.Domain.Entities.Common;

namespace TrackYourSpendings.Domain.Entities;

public class Transaction : BaseEntity
{
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
    [MaxLength(300)] public string? Description { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public decimal Cost { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid WalletId { get; set; }
    public Wallet? Wallet { get; set; }
}