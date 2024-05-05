using MediatR;
using TrackYourSpendings.Application.Dtos.Transactions;

namespace TrackYourSpendings.Application.Features.Transactions.Requests.Queries;

public class GetTransactionsRequest : IRequest<List<GetTransactionDto>>
{
    public Guid UserId { get; set; }
}