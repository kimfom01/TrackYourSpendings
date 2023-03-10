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
}