namespace TrackYourSpendings.Application.Dtos.Transactions;

public class CreateTransactionDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public Guid CategoryId { get; set; }
    public Guid WalletId { get; set; }
}