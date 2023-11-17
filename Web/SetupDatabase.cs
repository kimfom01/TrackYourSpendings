using Budget.Context;

namespace Web;

public static class SetupDatabase
{
    public static async Task ResetDatabase(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();

        // await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}