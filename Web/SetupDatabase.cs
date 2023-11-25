using Budget.Context;

namespace Web;

public static class SetupDatabase
{
    public static async Task ResetDatabase(IServiceScope scope, IWebHostEnvironment environment)
    {
        var context = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();

        if (environment.IsDevelopment())
        {
            await context.Database.EnsureDeletedAsync();
        }

        await context.Database.EnsureCreatedAsync();
    }
}