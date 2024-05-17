using MediatR;
using TrackYourSpendings.Application.Dtos.Transactions;

namespace TrackYourSpendings.Application.Features.Transactions.Requests.Commands;

public class UpdateTransactionRequest: IRequest<Unit>
{
    public Guid UserId { get; set; }
    public GetTransactionDto TransactionDto { get; set; }
}