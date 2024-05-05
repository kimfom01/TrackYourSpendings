using AutoMapper;
using TrackYourSpendings.Application.Dtos.Categories;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Application.MappingProfiles;

public class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<CategoryDto, Category>()
            .ReverseMap();
    }
}