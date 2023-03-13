namespace Budget.Models;

public class Category
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }

    public IEnumerable<Transaction>? Transactions { get; set; }
    public int WalletId { get; set; }
    public Wallet? Wallet { get; set; }
}