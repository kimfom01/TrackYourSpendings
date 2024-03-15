using System.Net;
using System.Net.Mail;
using System.Security;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using TrackYourSpendings.Web.Exceptions;
using TrackYourSpendings.Web.Utils;

namespace TrackYourSpendings.Web.Infrastructure;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;
    private readonly SmtpClient _smtpClient;

    public EmailSender(IConfiguration config, IWebHostEnvironment env)
    {
        _config = config;
        _env = env;
        var host = EnvironmentConfigHelper.GetEmailHost(config, env);
        var port = EnvironmentConfigHelper.GetEmailPort(config, env);
        var senderEmail = EnvironmentConfigHelper.GetEmailSenderEmail(config, env);
        var password = GetSecurePassword(EnvironmentConfigHelper.GetEmailPassword(config, env));

        _smtpClient = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(senderEmail, password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var sender = new SmtpSender(() => _smtpClient);

        Email.DefaultSender = sender;

        var sendResponse = await Email
            .From(EnvironmentConfigHelper.GetEmailSenderEmail(_config, _env))
            .To(email)
            .Subject(subject)
            .Body(htmlMessage)
            .SendAsync();

        if (!sendResponse.Successful)
        {
            throw new SendFailException("Failed to send email");
        }
    }

    private SecureString GetSecurePassword(string password)
    {
        var secureString = new SecureString();

        foreach (var character in password)
        {
            secureString.AppendChar(character);
        }

        return secureString;
    }
}