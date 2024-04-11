using System.Net;
using Xunit.Abstractions;

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

        response.EnsureSuccessStatusCode();
    }

    public class Wallet : BudgetTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Wallet(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper) : base(factory)
        {
            _testOutputHelper = testOutputHelper;
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