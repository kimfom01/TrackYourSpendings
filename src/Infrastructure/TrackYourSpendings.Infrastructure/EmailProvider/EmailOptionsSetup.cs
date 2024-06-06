using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TrackYourSpendings.Application.ConfigOptions;

namespace TrackYourSpendings.Infrastructure.EmailProvider;

public class EmailOptionsSetup : IConfigureNamedOptions<EmailOptions>
{
    private readonly IConfiguration _configuration;

    public EmailOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(EmailOptions options)
    {
        _configuration.GetSection(nameof(EmailOptions)).Bind(options);
    }

    public void Configure(string? name, EmailOptions options)
    {
        Configure(options);
    }
}