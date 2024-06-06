using Microsoft.AspNetCore.HttpOverrides;
using TrackYourSpendings.Application;
using TrackYourSpendings.Infrastructure;
using TrackYourSpendings.Web.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.ConfigureWebProjectServices(builder.Configuration, builder.Environment);
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

app.ApplyMigrations();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{walletId?}");

app.MapPrometheusScrapingEndpoint();

app.MapHealthChecks("/healthz");

app.Run();