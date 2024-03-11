using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TrackYourSpendings.Web.Models;

public class Wallet
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [Precision(10, 2)]
    public decimal Income { get; set; }

    [Precision(10, 2)]
    public decimal Expenses { get; set; } = 0M;
    
    [Precision(10, 2)]
    public decimal Balance { get; set; } = 0M;

    public bool Active { get; set; } = false;

    public IEnumerable<Transaction>? Transactions { get; set; }
    
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}