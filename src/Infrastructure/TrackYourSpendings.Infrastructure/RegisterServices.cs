using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackYourSpendings.Application.Contracts.Infrastructure.EmailProvider;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Infrastructure.Database;
using TrackYourSpendings.Infrastructure.EmailProvider;
using TrackYourSpendings.Infrastructure.Identity;

namespace TrackYourSpendings.Infrastructure;

public static class RegisterServices
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<AppDataContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });
        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("IdentityConnection"));
        });
        return services;
    }
}