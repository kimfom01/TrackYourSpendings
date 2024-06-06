using MediatR;
using TrackYourSpendings.Application.Dtos.Transactions;

namespace TrackYourSpendings.Application.Features.Transactions.Requests.Queries;

public class SearchAndFilterTransactionsRequest : IRequest<List<GetTransactionDto>>
{
    public string? UserId { get; set; }
    public Guid? WalletId { get; set; }
    public string? SearchString { get; set; }
    public Guid? CategoryId { get; set; }
    public DateTime? Date { get; set; }
}