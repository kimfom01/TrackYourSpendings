using AutoMapper;
using Web.Dtos;
using Web.Models;

namespace Web.MappingProfile;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TransactionDto, Transaction>()
            .ReverseMap();
    }
}