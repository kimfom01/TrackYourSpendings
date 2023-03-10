namespace Budget.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public required string TransactionName { get; set; }
    public required string TransactionDescription { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Cost { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int WalletId { get; set; }
    public Wallet? Wallet { get; set; }
}