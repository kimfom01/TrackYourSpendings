using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackYourSpendings.Infrastructure.Database;
using TrackYourSpendings.Infrastructure.Database.Identity;

namespace TrackYourSpendings.IntegrationTests.Utils;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDataContext>));
            
            var identityDbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppIdentityDbContext>));

            if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);
            
            if (identityDbContextDescriptor != null) services.Remove(identityDbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            if (dbConnectionDescriptor != null) services.Remove(dbConnectionDescriptor);

            services.AddDbContext<AppDataContext>(options => { options.UseInMemoryDatabase("testdb"); });
            services.AddDbContext<AppIdentityDbContext>(options => { options.UseInMemoryDatabase("testidentitydb"); });
        });

        builder.UseEnvironment("Development");
    }
}