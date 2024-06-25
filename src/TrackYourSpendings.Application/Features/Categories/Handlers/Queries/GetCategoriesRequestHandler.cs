using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Application.Dtos.Categories;
using TrackYourSpendings.Application.Features.Categories.Requests.Queries;

namespace TrackYourSpendings.Application.Features.Categories.Handlers.Queries;

public class GetCategoriesRequestHandler : IRequestHandler<GetCatetoriesRequest, List<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCategoriesRequestHandler> _logger;
    private readonly IMapper _mapper;

    public GetCategoriesRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetCategoriesRequestHandler> logger,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetCatetoriesRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting categories for user={userId}", request.UserId);
        var categories = await _unitOfWork.Categories.GetEntities(cat => true);

        if (categories is null)
        {
            _logger.LogWarning("No categories for user={userId}", request.UserId);
            return [];
        }

        return _mapper.Map<List<CategoryDto>>(categories);
    }
}