using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Context;

/// <summary>
/// Represents the database context for the application, integrating Identity framework support.
/// </summary>
/// <remarks>
/// This context includes sets for Categories, Transactions, and Wallets, alongside user management provided by IdentityDbContext.
/// </remarks>
public class DataContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Gets or sets the database set for Categories.
    /// </summary>
    /// <value>
    /// The DbSet representing the categories in the database.
    /// </value>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets the database set for Transactions.
    /// </summary>
    /// <value>
    /// The DbSet representing the transactions in the database.
    /// </value>
    public DbSet<Transaction> Transactions { get; set; }

    /// <summary>
    /// Gets or sets the database set for Wallets.
    /// </summary>
    /// <value>
    /// The DbSet representing the wallets in the database.
    /// </value>
    public DbSet<Wallet> Wallets { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataContext"/> class using the specified options.
    /// </summary>
    /// <param name="options">The options to be used by a DbContext.</param>
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures the schema needed for the application when the model is being created.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring a model that maps to a database.</param>
    /// <remarks>
    /// This method includes configuration for seeding the Category entities.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Comment: Seed initial data for categories into the database.
        base.OnModelCreating(builder);
        builder.Entity<Category>()
            .HasData(new Category
                {
                    Id = 1,
                    Name = "Housing"
                },
                new Category
                {
                    Id = 2,
                    Name = "Transportation"
                },
                new Category
                {
                    Id = 3,
                    Name = "Food"
                },
                new Category
                {
                    Id = 4,
                    Name = "Utilities"
                },
                new Category
                {
                    Id = 5,
                    Name = "Insurance"
                },
                new Category
                {
                    Id = 6,
                    Name = "Medical & Healthcare"
                },
                new Category
                {
                    Id = 7,
                    Name = "Saving, Investing & Dept Payments"
                },
                new Category
                {
                    Id = 8,
                    Name = "Personal Spending"
                },
                new Category
                {
                    Id = 9,
                    Name = "Recreation & Entertainment"
                },
                new Category
                {
                    Id = 10,
                    Name = "Miscellaneous"
                }
            );
    }
}