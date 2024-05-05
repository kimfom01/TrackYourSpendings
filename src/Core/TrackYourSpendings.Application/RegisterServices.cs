using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackYourSpendings.Application.ConfigOptions;

namespace TrackYourSpendings.Application;

public static class RegisterServices
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailOption>(configuration.GetSection(nameof(EmailOption)));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}