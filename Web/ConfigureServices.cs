using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Web.Context;
using Web.Models;
using Web.Repositories;
using Web.Repositories.Implementations;
using Web.Services;
using Web.Services.Implementation;

namespace Web;

public static class ConfigureServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config,
        IWebHostEnvironment env)
    {
        services.AddControllersWithViews();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(EnvironmentConfigHelper.GetConnectionString(config, env));
        });

        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<DataContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = EnvironmentConfigHelper.GetGoogleClientId(config, env);
                options.ClientSecret = EnvironmentConfigHelper.GetGoogleClientSecret(config, env);
                options.CallbackPath = EnvironmentConfigHelper.GetGoogleRedirectUri(config, env);
                options.SaveTokens = true;
            });

        return services;
    }
}