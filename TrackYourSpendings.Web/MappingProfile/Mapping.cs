using AutoMapper;
using TrackYourSpendings.Web.Dtos;
using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.MappingProfile;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TransactionDto, Transaction>()
            .ReverseMap();
    }
}