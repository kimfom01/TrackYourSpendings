using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Infrastructure.Database;

public class AppDataContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("app");
        // Comment: Seed initial data for categories into the database.
        modelBuilder.Entity<Category>()
            .HasData(new Category
                {
                    Id = new Guid("50d0672e-5a9f-41f2-9912-6eaccec3b09b"),
                    Name = "Housing"
                },
                new Category
                {
                    Id = new Guid("5cda7116-a260-4c68-b9db-2a596714346f"),
                    Name = "Transportation"
                },
                new Category
                {
                    Id = new Guid("a0ae659f-78e2-467e-a639-12a286f3cd74"),
                    Name = "Food"
                },
                new Category
                {
                    Id = new Guid("b91f92c3-1849-444c-915b-57ee11ecf757"),
                    Name = "Utilities"
                },
                new Category
                {
                    Id = new Guid("504becb7-60f9-41db-ae6a-ae98f7e6ccbc"),
                    Name = "Insurance"
                },
                new Category
                {
                    Id = new Guid("ec241f35-3d76-4f8c-9fc5-5af3f5a5872a"),
                    Name = "Medical & Healthcare"
                },
                new Category
                {
                    Id = new Guid("4d328b83-997f-4719-98d5-9713e9d5fcdb"),
                    Name = "Saving, Investing & Dept Payments"
                },
                new Category
                {
                    Id = new Guid("74140cff-4752-4a7d-8f86-699edc23f4a8"),
                    Name = "Personal Spending"
                },
                new Category
                {
                    Id = new Guid("227afb2b-7601-4171-ab24-7d5de3796351"),
                    Name = "Recreation & Entertainment"
                },
                new Category
                {
                    Id = new Guid("405eb333-07dc-4af0-86e3-fe3183e85c93"),
                    Name = "Miscellaneous"
                }
            );

        base.OnModelCreating(modelBuilder);
    }
}