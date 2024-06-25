using AutoMapper;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Application.MappingProfiles;

public class WalletMappings : Profile
{
    public WalletMappings()
    {
        CreateMap<WalletDto, Wallet>()
            .ReverseMap();
        CreateMap<WalletDetailDto, Wallet>()
            .ReverseMap();
        CreateMap<WalletCreateDto, Wallet>()
            .ReverseMap();
        CreateMap<WalletUpdateDto, Wallet>()
            .ReverseMap();
    }
}