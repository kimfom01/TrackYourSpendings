using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TrackYourSpendings.Web.Models;

public class Transaction
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(300)]
    public string? Description { get; set; }
    public Month? Month { get; set; }
    public DateTime? Date { get; set; }
    [Precision(10, 2)]
    public decimal Cost { get; set; } = 0M;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    [MaxLength(128)]
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}