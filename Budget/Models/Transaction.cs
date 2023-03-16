using Microsoft.EntityFrameworkCore;

namespace Budget.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public required string TransactionName { get; set; }
    public string? TransactionDescription { get; set; }
    public Month Month { get; set; }
    public DateTime TransactionDate { get; set; }
    
    [Precision(10, 2)]
    public decimal Cost { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}