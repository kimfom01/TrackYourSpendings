using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using TrackYourSpendings.IntegrationTests.Utils;
using TrackYourSpendings.Web;

namespace TrackYourSpendings.IntegrationTests;

public class UnauthorisedClientTests : IClassFixture<CustomWebApplicationFactory<ITestsEntry>>
{
    private readonly HttpClient _client;

    public UnauthorisedClientTests(CustomWebApplicationFactory<ITestsEntry> webApplicationFactory)
    {
        _client = webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/Home")]
    [InlineData("/Home/Privacy")]
    [InlineData("/Identity/Account/Login")]
    [InlineData("/Identity/Account/Register")]
    public async Task Get_PagesReturnSuccess(string url)
    {
        var response = await _client.GetAsync(url);

        var message = response.EnsureSuccessStatusCode();
        
        Assert.True(message.IsSuccessStatusCode);
    }

    [Theory]
    [InlineData("/Budget")]
    [InlineData("/Reports")]
    public async Task Get_PagesReturnRedirectToLogin(string url)
    {
        var response = await _client.GetAsync(url);

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    }

    [Theory]
    [InlineData("/Identity/Account/Login")]
    [InlineData("/Identity/Account/Register")]
    public async Task Get_AuthPagesReturnsForm(string url)
    {
        var response = await _client.GetAsync(url);

        Assert.Contains("<form", await response.Content.ReadAsStringAsync());
    }
}