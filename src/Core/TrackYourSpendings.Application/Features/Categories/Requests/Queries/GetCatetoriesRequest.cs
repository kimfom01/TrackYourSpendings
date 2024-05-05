using MediatR;
using TrackYourSpendings.Application.Dtos.Categories;

namespace TrackYourSpendings.Application.Features.Categories.Requests.Queries;

public class GetCatetoriesRequest : IRequest<List<CategoryDto>>
{
    public Guid UserId { get; set; }
}