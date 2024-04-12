using System.Net;
using TrackYourSpendings.IntegrationTests.Utils;

namespace TrackYourSpendings.IntegrationTests;

public class BudgetTest : BaseAuth
{
    public BudgetTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("/Budget")]
    [InlineData("/Budget/Index")]
    public async Task Get_ReturnSecurePage(string url)
    {
        var response = await Client.GetAsync(url);

        var message = response.EnsureSuccessStatusCode();
        
        Assert.True(message.IsSuccessStatusCode);
    }

    public class Wallet : BudgetTest
    {
        public Wallet(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Post_AddNewWallet_RedirectToBudget()
        {
            var response = await Client.PostAsync("/Budget/AddWallet",
                new FormUrlEncodedContent([
                    new KeyValuePair<string, string>("Name", "Test Wallet"),
                    new KeyValuePair<string, string>("Currency", "RUB"),
                    new KeyValuePair<string, string>("Income", "10000")
                ]));

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Budget", response.Headers.Location?.OriginalString);
        }
    }
}