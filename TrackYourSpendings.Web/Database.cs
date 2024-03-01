using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Web.Context;

namespace TrackYourSpendings.Web;

public static class Database
{
    public static async Task SetupDatabase(IServiceScope scope, IWebHostEnvironment environment)
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        if (environment.IsDevelopment())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
        else
        {
            await context.Database.MigrateAsync();
        }
    }
}