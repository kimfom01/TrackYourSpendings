using AutoMapper;
using TrackYourSpendings.Application.Dtos.Transactions;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Application.MappingProfiles;

public class TransactionMappings : Profile
{
    public TransactionMappings()
    {
        CreateMap<CreateTransactionDto, Transaction>()
            .ReverseMap();
        CreateMap<GetTransactionDto, Transaction>()
            .ReverseMap();
    }
}