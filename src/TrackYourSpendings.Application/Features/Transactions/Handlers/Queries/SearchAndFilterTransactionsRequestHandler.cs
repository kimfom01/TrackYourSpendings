using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Dtos.Transactions;
using TrackYourSpendings.Application.Features.Transactions.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Transactions.Handlers.Queries;

public class SearchAndFilterTransactionsRequestHandler
    : IRequestHandler<SearchAndFilterTransactionsRequest,
        List<GetTransactionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<SearchAndFilterTransactionsRequestHandler> _logger;

    public SearchAndFilterTransactionsRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<SearchAndFilterTransactionsRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetTransactionDto>> Handle(SearchAndFilterTransactionsRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Applying search and filter from request");

        var transactions =
            await _unitOfWork.Transactions.GetTransactionsWithCategories(request.WalletId, request.UserId);

        if (transactions is null)
        {
            _logger.LogInformation("There's no transaction to apply filter on");
            return [];
        }

        if (request.SearchString is not null)
        {
            _logger.LogInformation("Applying search from request");
            transactions = transactions.Where(tr =>
                tr.Name.Contains(request.SearchString, StringComparison.InvariantCultureIgnoreCase));
        }

        if (request.CategoryId is not null)
        {
            _logger.LogInformation("Applying category filter from request");
            transactions = transactions.Where(tr => tr.CategoryId == request.CategoryId);
        }

        if (request.Date is not null)
        {
            _logger.LogInformation("Applying date filter from request");
            transactions = transactions.Where(tr => tr.Date == request.Date);
        }

        _logger.LogInformation("All search and filter applied");
        return _mapper.Map<List<GetTransactionDto>>(transactions);
    }
}