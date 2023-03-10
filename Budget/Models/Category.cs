namespace Budget.Models;

public class Category
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }
}