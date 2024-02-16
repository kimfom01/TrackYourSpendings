using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
await SetupDatabase.ResetDatabase(scope, builder.Environment);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();