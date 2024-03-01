using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Dtos;

public class TransactionDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Month? Month { get; set; }
    public decimal? Cost { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int WalletId { get; set; }
}