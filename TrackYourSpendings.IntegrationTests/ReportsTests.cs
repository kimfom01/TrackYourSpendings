using TrackYourSpendings.IntegrationTests.Utils;

namespace TrackYourSpendings.IntegrationTests;

public class ReportsTests : BaseAuth
{
    public ReportsTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("/Reports")]
    public async Task Get_ReturnSecurePage(string url)
    {
        var response = await Client.GetAsync(url);

        var message = response.EnsureSuccessStatusCode();
        
        Assert.True(message.IsSuccessStatusCode);
    }
}