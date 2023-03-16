using Microsoft.EntityFrameworkCore;

namespace Budget.Models;

public class Wallet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    [Precision(10, 2)]
    public decimal Income { get; set; }
    
    [Precision(10, 2)]
    public decimal? Expenses { get; set; }
    
    [Precision(10, 2)]
    public decimal? Balance { get; set; }
    
    public IEnumerable<Category>? Categories { get; set; }
}