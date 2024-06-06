using FluentEmail.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using TrackYourSpendings.Application.Exceptions;

namespace TrackYourSpendings.Infrastructure.EmailProvider;

public class EmailSender : IEmailSender
{
    private readonly IFluentEmail _fluentEmail;

    public EmailSender(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var sendResponse = await _fluentEmail
            .To(email)
            .Subject(subject)
            .UsingTemplate(htmlMessage, new { })
            .SendAsync();

        if (!sendResponse.Successful)
        {
            throw new SendFailException("Failed to send email");
        }
    }
}