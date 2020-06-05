namespace SoccerSolutionsApp.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class PredictionsPageTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;
        private readonly WebApplicationFactory<Startup> server;

        public PredictionsPageTests(WebApplicationFactory<Startup> server)
        {
            this.server = server;
            this.httpClient = this.server.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }
    }
}
