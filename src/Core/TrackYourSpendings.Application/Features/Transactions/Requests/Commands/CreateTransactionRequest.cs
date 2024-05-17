using MediatR;
using TrackYourSpendings.Application.Dtos.Transactions;

namespace TrackYourSpendings.Application.Features.Transactions.Requests.Commands;

public class CreateTransactionRequest : IRequest<GetTransactionDto>
{
    public CreateTransactionDto TransactionDto { get; set; }
    public Guid UserId { get; set; }
}