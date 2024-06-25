using TrackYourSpendings.Application.Dtos.Common;

namespace TrackYourSpendings.Application.Dtos.Categories;

public class CategoryDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
}