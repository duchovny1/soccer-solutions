namespace SoccerSolutionsApp.Web.Tests.Integration
{
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;

    using Xunit;

    public class WebTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> server;

        public WebTests(CustomWebApplicationFactory<Startup> server)
        {
            this.server = server;
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public async Task IndexPageShouldReturnStatusCode200WithTitle()
        {
            var client = this.server.CreateClient();
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("<title>", responseContent);
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public async Task AccountManagePageRequiresAuthorization()
        {
            var client = this.server.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            var response = await client.GetAsync("Identity/Account/Manage");
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }


        [Theory]
        [InlineData("https://localhost:44319/api/data/postcountries")]
        [InlineData("https://localhost:44319/api/data/postseasons")]
        [InlineData("https://localhost:44319/api/data/postleagues/england/2018")]
        [InlineData("https://localhost:44319/api/data/postteams/2")]
        [InlineData("https://localhost:44319/api/data/postfixtures/524")]
        [InlineData("https://localhost:44319/api/data/postnextfixtures/2/")]
        [InlineData("https://localhost:44319/api/data/posth2h/2/3")]
        [InlineData("https://localhost:44319/api/data/poststandings/2")]
        [InlineData("https://localhost:44319/api/data/poststatistics/2/3")]
        public async Task ApiUrlsRequireAuthorization(string url)
        {
            var client = this.server.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Account/Login", response.Headers.Location.OriginalString);
        }

        [Fact]
        public async Task HomePageShouldReturnCountriesWithLeagues()
        {
            var client = this.server.CreateClient();
            var response = await client.GetAsync("/");
            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains("<span>England - Premier League</span>", responseAsString);
            Assert.Contains("<span>England - Championship</span>", responseAsString);

        }
    }
}
