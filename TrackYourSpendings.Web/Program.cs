using Microsoft.AspNetCore.HttpOverrides;
using TrackYourSpendings.Web.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

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