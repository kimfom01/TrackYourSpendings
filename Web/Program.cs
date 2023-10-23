using Budget.Context;
using Budget.Repositories;
using Budget.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BudgetDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BudgetDb"));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration.GetValue<string>("Google:CLIENT_ID")
        ?? throw new NullReferenceException("CLIENT_ID missing");
    options.ClientSecret = builder.Configuration.GetValue<string>("Google:CLIENT_SECRET")
        ?? throw new NullReferenceException("CLIENT_SECRET missing");
    options.CallbackPath = builder.Configuration.GetValue<string>("Google:REDIRECT_URI")
        ?? throw new NullReferenceException("REDIRECT_URI missing");
    options.SaveTokens = true;
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
await context.Database.EnsureCreatedAsync();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=SignIn}/{id?}");

app.Run();