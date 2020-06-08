namespace SoccerSolutionsApp.Web.HostedServices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using RestSharp;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Web.Infrastructure;

    public class DataProcessingService : IDataProcessingService, IDisposable
    {
        private const string LoggingMessage = "{0} newly added fixtures for league {1} at {2}";

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

        public string FilePath { get; } = Path.Combine(Environment.CurrentDirectory + "\\" + "fixturesinfo.txt");

        public void Dispose()
        {
            this.cancellationToken.Cancel();
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            using var sw = new StreamWriter(this.FilePath, true);

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var id in this.leaguesIds)
                {
                    // getting for now only fixures for popular leagues
                    // leagues with id more than 150 are non-popular
                    // and there's some bug with deserializing Json file
                    // BUG will be fixed ASAP

                    int result = this.GetFixtures(id);
                    string message = string.Format(LoggingMessage, result, id, DateTime.UtcNow);
                    this.logger.LogInformation(
                             message);
                    sw.WriteLine(message);
                }

                await Task.Delay(new TimeSpan(12, 00, 00));
            }

            sw.Close();
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
