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

        response.EnsureSuccessStatusCode();
    }
}