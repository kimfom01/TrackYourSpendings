using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace TrackYourSpendings.Infrastructure.Database;

public class AppDataContextFactory : IDesignTimeDbContextFactory<AppDataContext>
{
    private readonly IConfiguration _configuration;

    public AppDataContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDataContextFactory()
    {
        
    }

    public AppDataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDataContext>();

        builder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
            options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "app"));

        return new AppDataContext(builder.Options);
    }
}