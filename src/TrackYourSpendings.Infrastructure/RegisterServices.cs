using System.Net;
using System.Net.Mail;
using System.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TrackYourSpendings.Application.ConfigOptions;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Infrastructure.Database;
using TrackYourSpendings.Infrastructure.Database.Identity;
using TrackYourSpendings.Infrastructure.EmailProvider;

namespace TrackYourSpendings.Infrastructure;

public static class RegisterServices
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration config, bool isDevelopment)
    {
        services.ConfigureOptions<EmailOptionsSetup>();

        var emailOptions = services.BuildServiceProvider()
            .GetRequiredService<IOptions<EmailOptions>>().Value;

        if (isDevelopment)
        {
            services
                .AddFluentEmail(emailOptions.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender("localhost", 1025);
        }
        else
        {
            services
                .AddFluentEmail(emailOptions.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender(new SmtpClient(emailOptions.Host, emailOptions.Port)
                {
                    Credentials = new NetworkCredential(emailOptions.SenderEmail,
                        GetSecurePassword(emailOptions.Password)),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                });
        }

        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<AppDataContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                opt => opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "app"));
        });
        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                opt => opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "identity"));
        });
        return services;
    }

    private static SecureString GetSecurePassword(string password)
    {
        var secureString = new SecureString();

        foreach (var character in password)
        {
            secureString.AppendChar(character);
        }

        return secureString;
    }
}