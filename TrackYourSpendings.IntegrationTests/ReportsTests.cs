using TrackYourSpendings.IntegrationTests.Utils;
using TrackYourSpendings.Web;

namespace TrackYourSpendings.IntegrationTests;

public class ReportsTests : BaseAuth
{
    public ReportsTests(CustomWebApplicationFactory<ITestsEntry> factory) : base(factory)
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