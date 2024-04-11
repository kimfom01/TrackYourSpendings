using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TrackYourSpendings.IntegrationTests.Utils;

namespace TrackYourSpendings.IntegrationTests;

public abstract class BaseAuth : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly HttpClient Client;

    protected BaseAuth(CustomWebApplicationFactory<Program> factory)
    {
        Client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(defaultScheme: "TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "TestScheme", options => { });
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
    }
}