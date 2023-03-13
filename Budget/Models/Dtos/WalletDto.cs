namespace Budget.Models.Dtos;

public class WalletDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expenditure { get; set; }
    public decimal Balance { get; set; }
    
    public IEnumerable<Category>? Categories { get; set; }
    public int SelectedCategoryId { get; set; }
}