using MediatR;
using TrackYourSpendings.Application.Dtos.Transactions;

namespace TrackYourSpendings.Application.Features.Transactions.Requests.Commands;

public class DeleteTransactionRequest : IRequest<Unit>
{
    public string? UserId { get; set; }
    public GetTransactionDto? TransactionDto { get; set; }
}