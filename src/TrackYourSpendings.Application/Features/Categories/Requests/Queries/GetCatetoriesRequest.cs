using MediatR;
using TrackYourSpendings.Application.Dtos.Categories;

namespace TrackYourSpendings.Application.Features.Categories.Requests.Queries;

public class GetCatetoriesRequest : IRequest<List<CategoryDto>>
{
    public string? UserId { get; set; }
}