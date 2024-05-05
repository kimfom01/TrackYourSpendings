using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Persistence.Database;

namespace TrackYourSpendings.Persistence;

public static class RegisterServices
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
        IConfiguration config)

    {
        

        return services;
    }
}