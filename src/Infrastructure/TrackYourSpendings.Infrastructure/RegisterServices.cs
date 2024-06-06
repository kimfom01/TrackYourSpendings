using System.Net;
using System.Net.Mail;
using System.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackYourSpendings.Application.ConfigOptions;
using TrackYourSpendings.Application.Contracts.Persistence;
using TrackYourSpendings.Infrastructure.Database;
using TrackYourSpendings.Infrastructure.Database.Identity;
using TrackYourSpendings.Infrastructure.EmailProvider;

namespace TrackYourSpendings.Infrastructure;

public static class RegisterServices
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration config, bool isDevelopment)
    {
        var emailOption = new EmailOption();

        config.GetSection(nameof(emailOption)).Bind(emailOption);

        if (isDevelopment)
        {
            services
                .AddFluentEmail(emailOption.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender("localhost", 1025);
        }
        else
        {
            services
                .AddFluentEmail(emailOption.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender(new SmtpClient(emailOption.Host, emailOption.Port)
                {
                    Credentials = new NetworkCredential(emailOption.SenderEmail,
                        GetSecurePassword(emailOption.Password)),
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