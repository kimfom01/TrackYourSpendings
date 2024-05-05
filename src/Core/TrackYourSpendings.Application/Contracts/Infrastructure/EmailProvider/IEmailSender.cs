namespace TrackYourSpendings.Application.Contracts.Infrastructure.EmailProvider;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}