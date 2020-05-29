namespace SoccerSolutionsApp.Web.HostedServices
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using RestSharp;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;

    public class DataProviderHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<DataProviderHostedService> logger;
        private Timer timer;
        private IFixturesService fixturesService;
        private ILeaguesService leaguesService;
        private IEnumerable<int> leaguesIds;

        public DataProviderHostedService(
            FixturesService fixturesService,
            LeaguesService leaguesService,
            ILogger<DataProviderHostedService> logger)
        {
            this.logger = logger;
            this.fixturesService = fixturesService;
            this.leaguesService = leaguesService;
            this.InitializeLeagueIds();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // on every 12 hours checking for new information
            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            foreach (var id in this.leaguesIds)
            {
                var result = this.GetFixture(id);

                this.logger
                    .LogInformation($"For league with id {id} has been added {result} new fixtures at {DateTime.UtcNow}");
            }
        }

        private int GetFixture(int leagueId)
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/fixtures/league/{leagueId}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");
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

        private void InitializeLeagueIds()
        {
            this.leaguesIds = this.leaguesService.GetAllLeaguesId();
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
