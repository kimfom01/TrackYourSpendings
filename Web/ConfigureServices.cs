using Budget.Context;
using Budget.Repositories;
using Budget.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

namespace Web;

public static class ConfigureServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        services.AddControllersWithViews();
        services.AddDbContext<BudgetDbContext>(options =>
        {
            options.UseNpgsql(EnvironmentConfigHelper.GetConnectionString(config, env));
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie()
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