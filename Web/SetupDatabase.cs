using Microsoft.EntityFrameworkCore;
using Web.Context;

namespace Web;

public static class SetupDatabase
{
    public static async Task ResetDatabase(IServiceScope scope, IWebHostEnvironment environment)
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        if (environment.IsDevelopment())
        {
            await context.Database.EnsureDeletedAsync();
        }

        await context.Database.MigrateAsync();
    }
}