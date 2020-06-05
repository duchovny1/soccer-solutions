using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SoccerSolutionsApp.Services.Data.Fixtures;
using SoccerSolutionsApp.Services.Data.Leagues;
using SoccerSolutionsApp.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerSolutionsApp.Web.HostedServices
{
    internal class DataProcessingService : IDataProcessingService
    {
        private IEnumerable<int> leaguesIds;

        private readonly ILogger logger;
        private readonly ILeaguesService leaguesService;
        private readonly FixturesService fixturesService;

        internal DataProcessingService(
            ILogger<DataProcessingService> logger,
            LeaguesService leaguesService,
            FixturesService fixturesService)
        {
            this.logger = logger;
            this.leaguesService = leaguesService;
            this.fixturesService = fixturesService;
            this.InitializeIds();
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var id in this.leaguesIds)
                {
                    int result = this.GetFixtures(id);
                    this.logger.LogInformation(
                             $"{result} newly added fixtures for league {id}");
                }

                await Task.Delay(new TimeSpan(12, 00, 00));

            }
        }

        private int GetFixtures(int leagueId)
        {
            var client = new RestClient(string.Format(DataProvider.GetFixturesByIdUrl, leagueId));

            var request = new RestRequest(Method.GET);
            request.AddHeader(DataProvider.ApiHost, DataProvider.ApiHostValue);
            request.AddHeader(DataProvider.ApiKey, DataProvider.ApiKeyValue);
            IRestResponse response = client.Execute(request);
            string content = response.Content;
            if (response.IsSuccessful)
            {
                var fixtures = JsonConvert.DeserializeObject<ImportFixturesApi>(content);
                int result = this.fixturesService.Create(fixtures);

                return result;
            }

            return 0;
        }

        private void InitializeIds()
        {
            this.leaguesIds = this.leaguesService.GetAllLeaguesId();
        }

    }
}
