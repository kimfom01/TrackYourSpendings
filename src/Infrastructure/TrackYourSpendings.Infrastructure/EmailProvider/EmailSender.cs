using System.Net;
using System.Net.Mail;
using System.Security;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Options;
using TrackYourSpendings.Application.ConfigOptions;
using TrackYourSpendings.Application.Contracts.Infrastructure.EmailProvider;
using TrackYourSpendings.Application.Exceptions;

namespace TrackYourSpendings.Infrastructure.EmailProvider;

public class EmailSender : IEmailSender
{
    private readonly EmailOption _emailOption;
    private readonly SmtpClient _smtpClient;

    public EmailSender(IOptions<EmailOption> options)
    {
        _emailOption = options.Value;

        _smtpClient = new SmtpClient(_emailOption.Host, _emailOption.Port)
        {
            Credentials = new NetworkCredential(_emailOption.SenderEmail, GetSecurePassword(_emailOption.Password)),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var sender = new SmtpSender(() => _smtpClient);

        Email.DefaultSender = sender;

        var sendResponse = await Email
            .From(_emailOption.SenderEmail)
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