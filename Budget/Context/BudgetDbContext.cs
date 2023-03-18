using Budget.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget.Context;

public class BudgetDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>()
            .HasData(new Wallet
                {
                    Id = 1,
                    Name = "Test Wallet",
                    Income = 1500.00M,
                    Expenses = 650M,
                    Balance = 850M
                },
                new Wallet
                {
                    Id = 2,
                    Name = "Main Wallet",
                    Income = 1500.00M,
                    Expenses = 0,
                    Balance = 1500M
                });

        modelBuilder.Entity<Category>()
            .HasData(new Category
                {
                    CategoryId = 1,
                    WalletId = 1,
                    CategoryName = "Gadgets"
                }, new Category
                {
                    CategoryId = 2,
                    WalletId = 1,
                    CategoryName = "Groceries"
                });

        modelBuilder.Entity<Transaction>()
            .HasData(new Transaction
                {
                    Id = 1,
                    Name = "Computer Accessories",
                    Description = "I bought a new laptop, external keyboard and mouse",
                    Date = DateTime.Now,
                    Cost = 500.00M,
                    CategoryId = 1
                },
                new Transaction
                {
                    Id = 2,
                    Name = "Weekly fruit stocking",
                    Description = "I bought a bunch of bananas, grapes and 7 oranges",
                    Date = DateTime.Now,
                    Cost = 150.00M,
                    CategoryId = 2
                });
    }
}