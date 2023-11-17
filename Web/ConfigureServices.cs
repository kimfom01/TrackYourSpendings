using Budget.Context;
using Budget.Repositories;
using Budget.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

namespace Web;

public static class ConfigureServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllersWithViews();
        services.AddDbContext<BudgetDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("BudgetDb"));
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = config.GetValue<string>("Google:CLIENT_ID")
                                   ?? throw new NullReferenceException("CLIENT_ID missing");
                options.ClientSecret = config.GetValue<string>("Google:CLIENT_SECRET")
                                       ?? throw new NullReferenceException("CLIENT_SECRET missing");
                options.CallbackPath = config.GetValue<string>("Google:REDIRECT_URI")
                                       ?? throw new NullReferenceException("REDIRECT_URI missing");
                options.SaveTokens = true;
            });
        
        return services;
    }
}