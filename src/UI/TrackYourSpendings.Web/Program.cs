using Microsoft.AspNetCore.HttpOverrides;
using TrackYourSpendings.Application;
using TrackYourSpendings.Infrastructure;
using TrackYourSpendings.Web.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureWebProjectServices(builder.Configuration, builder.Environment);
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
await Database.SetupDatabase(scope, builder.Environment);

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

// TODO: Create default wallets for each month of the year, discuss this with Suwilanji.