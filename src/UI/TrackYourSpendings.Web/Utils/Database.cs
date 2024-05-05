using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Infrastructure.Database;
using TrackYourSpendings.Infrastructure.Identity;

namespace TrackYourSpendings.Web.Utils;

public static class Database
{
    public static async Task SetupDatabase(IServiceScope scope, IWebHostEnvironment environment)
    {
        var appDataContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();
        var appIdentityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

        if (environment.IsDevelopment())
        {
            await appDataContext.Database.EnsureDeletedAsync();
            await appDataContext.Database.EnsureCreatedAsync();
            await appIdentityDbContext.Database.EnsureDeletedAsync();
            await appIdentityDbContext.Database.EnsureCreatedAsync();
        }
        else
        {
            await appDataContext.Database.MigrateAsync();
            await appIdentityDbContext.Database.MigrateAsync();
        }
    }
}