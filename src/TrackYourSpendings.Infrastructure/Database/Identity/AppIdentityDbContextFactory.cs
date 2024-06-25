using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace TrackYourSpendings.Infrastructure.Database.Identity;

public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
{
    private readonly IConfiguration _configuration;

    public AppIdentityDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppIdentityDbContextFactory()
    {
        
    }

    public AppIdentityDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppIdentityDbContext>();

        builder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
            options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "identity"));

        return new AppIdentityDbContext(builder.Options);
    }
}