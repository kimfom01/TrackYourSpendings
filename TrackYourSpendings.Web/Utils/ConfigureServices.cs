using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TrackYourSpendings.Web.Context;
using TrackYourSpendings.Web.Infrastructure;
using TrackYourSpendings.Web.Models;
using TrackYourSpendings.Web.Repositories;
using TrackYourSpendings.Web.Repositories.Implementations;
using TrackYourSpendings.Web.Services;
using TrackYourSpendings.Web.Services.Implementation;

namespace TrackYourSpendings.Web.Utils;

public static class ConfigureServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config,
        IWebHostEnvironment env)
    {
        var tracingOtlpEndpoint = config["OTLP_ENDPOINT_URL"];
        var otel = services.AddOpenTelemetry();

        otel.ConfigureResource(res => res.AddService(serviceName: env.ApplicationName));

        otel.WithMetrics(metrics =>
            metrics
                .AddAspNetCoreInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddPrometheusExporter()
        );

        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            if (tracingOtlpEndpoint is not null)
            {
                tracing.AddOtlpExporter(otlpOptions => { otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint); });
            }
            else
            {
                tracing.AddConsoleExporter();
            }
        });

        services.AddControllersWithViews().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(EnvironmentConfigHelper.GetConnectionString(config, env));
        });

        services.AddDefaultIdentity<ApplicationUser>(options =>
                options.SignIn.RequireConfirmedAccount = !env.IsDevelopment())
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
        services.AddHealthChecks()
            .AddDbContextCheck<DataContext>();
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}