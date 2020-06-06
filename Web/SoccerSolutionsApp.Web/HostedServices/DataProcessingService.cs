using Microsoft.Extensions.DependencyInjection;
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
    public class DataProcessingService : IDataProcessingService, IDisposable
    {
        private readonly ILogger logger;
        private readonly CancellationTokenSource cancellationToken =
                                                    new CancellationTokenSource();

        private ILeaguesService leaguesService;
        private IFixturesService fixturesService;
        private IEnumerable<int> leaguesIds;

        public DataProcessingService(
            IServiceProvider services,
            ILogger<DataProcessingService> logger)
        {
            this.Services = services;
            this.logger = logger;
            this.InitializeServices();
            this.InitializeIds();
        }

        public IServiceProvider Services { get; set; }

        public void Dispose()
        {
            this.cancellationToken.Cancel();
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var id in this.leaguesIds)
                {

                    // getting for now only fixures for popular leagues
                    // leagues with id more than 150 are non-popular
                    // and there's some bug with deserializing Json file
                    // BUG will be fixed ASAP
                    if (id > 150)
                    {
                        break;
                    }

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

        private void InitializeServices()
        {

            var leaguesService = this.Services.GetRequiredService<ILeaguesService>();
            this.leaguesService = leaguesService;

            var fixturesService = this.Services.GetRequiredService<IFixturesService>();
            this.fixturesService = fixturesService;
        }
    }
}
