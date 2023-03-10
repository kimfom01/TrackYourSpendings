namespace Budget.Models;

public class Wallet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expenditure { get; set; }
    public decimal Balance { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }
}