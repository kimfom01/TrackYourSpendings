using AutoMapper;
using Budget.Models;
using Budget.Models.Dtos;

namespace Budget.Mappings;

public class AutoMappings : Profile
{
    public AutoMappings()
    {
        CreateMap<Wallet, WalletDto>()
            .ForMember(walDto => walDto.Categories, opt =>
                opt.MapFrom(wal => wal.Categories))
            .ReverseMap();
    }
}