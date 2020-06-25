namespace SoccerSolutionsApp.Web.Tests.Integration
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using Xunit;

    public class WebTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> server;
        private readonly UserManager<ApplicationUser> userManager;

        public WebTests(CustomWebApplicationFactory<Startup> server)
        {
            this.server = server;
        }

        [Fact]
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
        public async Task IndexPageShouldReturnCountriesWithLeagues()
        {
            var client = this.server.CreateClient();
            var response = await client.GetAsync("/");
            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains("<span>England - Premier League</span>", responseAsString);
            Assert.Contains("<span>England - Championship</span>", responseAsString);

        }

        [Fact]
        public async Task TestCreatePostPredictions()
        {
            var client = this.server.CreateClient();

            var request = new
            {
                Url = "/Predictions/Create",
                Body = new
                {
                    CountryId = 1,
                    LeagueId = 2,
                    UserId = "1",
                    Content = "Game should end over 2.5",
                    FixtureId = 1,
                    GamePrediction = "Over 2.5",
                },
            };

            var postResponse = await client.PostAsync(
                request.Url,
                new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8)
                {
                    Headers =
                    {
                        ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"),
                    },
                });

            postResponse.EnsureSuccessStatusCode();

            //var response = await client.GetAsync("Predictions/All");
            //var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Game should end over 2.5", await postResponse.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task TestIfShowAllPredictions()
        {
            var client = this.server.CreateClient();
            var response = await client.GetAsync("Predictions/All");
            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Game should be 1", responseAsString);
            Assert.Contains("Game should be 2", responseAsString);
        }
    }
}
