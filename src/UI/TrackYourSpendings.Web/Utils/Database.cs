using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Infrastructure.Database;
using TrackYourSpendings.Infrastructure.Database.Identity;

namespace TrackYourSpendings.Web.Utils;

public static class Database
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var appDataContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();
        var appIdentityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

        appDataContext.Database.Migrate();
        appIdentityDbContext.Database.Migrate();

        return app;
    }
}