using System.Text.Json.Serialization;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TrackYourSpendings.Infrastructure.Database;
using TrackYourSpendings.Infrastructure.Identity;

namespace TrackYourSpendings.Web.Utils;

public static class RegisterWebProjectServices
{
    public static IServiceCollection ConfigureWebProjectServices(
        this IServiceCollection services,
        IConfiguration config,
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
        services.AddHealthChecks()
            .AddDbContextCheck<AppDataContext>();
        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = !env.IsDevelopment();
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>();
        services.AddAuthentication();

        return services;
    }
}