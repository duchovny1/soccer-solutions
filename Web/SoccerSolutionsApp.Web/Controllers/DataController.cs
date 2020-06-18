namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Authenticators;
    using SoccerSolutionsApp.Common;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Data.Seasons;
    using SoccerSolutionsApp.Services.Data.Standings;
    using SoccerSolutionsApp.Services.Data.Teams;
    using SoccerSolutionsApp.Services.Data.Teams.ImportStatistics;
    using SoccerSolutionsApp.Services.Data.TeamsServices;
    using SoccerSolutionsApp.Services.Data.TeamStatistics;
    using SoccerSolutionsApp.Web.Infrastructure;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly ICountriesService countriesService;
        private readonly ISeasonsService seasonsService;
        private readonly ILeaguesService leaguesService;
        private readonly ITeamsService teamsService;
        private readonly IConfiguration configuration;
        private readonly IFixturesService fixturesService;
        private readonly IStandingsService standingsService;
        private readonly ITeamStatisticsService teamStatisticsService;

        public DataController(
            ApplicationDbContext db,
            ICountriesService countriesService,
            ISeasonsService seasonsService,
            ILeaguesService leaguesService,
            ITeamsService teamsService,
            IConfiguration configuration,
            IFixturesService fixturesService,
            IStandingsService standingsService,
            ITeamStatisticsService teamStatisticsService)
        {
            this.db = db;
            this.countriesService = countriesService;
            this.seasonsService = seasonsService;
            this.leaguesService = leaguesService;
            this.teamsService = teamsService;
            this.configuration = configuration;
            this.fixturesService = fixturesService;
            this.standingsService = standingsService;
            this.teamStatisticsService = teamStatisticsService;
        }

        [HttpGet("getleagues")]
        // it may need to be with attribute allow anonymous
        public ActionResult<SelectList> GetLeagues(int countryId)
        {
            // its used by cascading drop down menu in the view
            var leagues = new SelectList(this.db.Leagues.Where(c => c.CountryId == countryId && c.Season.StartYear == "2019").ToList(), "Id", "Name");

            return leagues;
        }

        [HttpGet("getfixtures")]
        public ActionResult<SelectList> GetFixtures(int leagueId)
        {
            var fixturesForNextNthDays = this.fixturesService.GetNextFixturesByLeagueIdAndDaysAsync(leagueId, 7);
            var fixtures = new SelectList(fixturesForNextNthDays.ToList(), "Id", "Name");

            return fixtures;
        }

        // getting data from external Api

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postcountries")]
        public IActionResult Countries()
        {
            var client = new RestClient(DataProvider.GetCountriesUrl);

            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);
            IRestResponse response = client.Execute(request);
            string content = response.Content;

            if (response.IsSuccessful)
            {
                var countries = JsonConvert.DeserializeObject<ImportCountriesApi>(content);
                int result = this.countriesService.Create(countries);

                return this.Ok($"{result} countries added");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postseasons")]
        public IActionResult Seasons()
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/seasons");

            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);

            IRestResponse response = client.Execute(request);
            string content = response.Content;

            if (response.IsSuccessful)
            {
                var seasons = JsonConvert.DeserializeObject<ImportSeasonsApi>(content);
                int result = this.seasonsService.Create(seasons);

                return this.Ok($"{result} seasons added");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postleagues/{countryName}/{season}")]
        public IActionResult LeaguesByCountryAndYear(string countryName, int season)
        {
            var client = new RestClient(string.Format(DataProvider.GetLeaguesByCountryUrl, countryName, season));

            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);

            IRestResponse response = client.Execute(request);
            string content = response.Content;

            if (response.IsSuccessful)
            {
                var leagues = JsonConvert.DeserializeObject<ImportLeaguesApi>(content);
                int result = this.leaguesService.Create(leagues);

                return this.Ok($"{result} leagues added");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postteams/{leagueId}")]
        public IActionResult GetTeams(int leagueId)
        {
            var client = new RestClient(string.Format(DataProvider.GetTeamsByIdUrl, leagueId));

            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);

            IRestResponse response = client.Execute(request);
            string content = response.Content;

            if (response.IsSuccessful)
            {
                var teams = JsonConvert.DeserializeObject<ImportTeamsApi>(content);
                int result = this.teamsService.Create(teams, leagueId);

                return this.Ok($"{result} teams added");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postfixtures/{leagueId}")]
        public IActionResult GetFixture(int leagueId)
        {
            var client = new RestClient(string.Format(DataProvider.GetFixturesByIdUrl, leagueId));
            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);
            IRestResponse response = client.Execute(request);
            string content = response.Content;
            if (response.IsSuccessful)
            {
                var fixtures = JsonConvert.DeserializeObject<ImportApi>(content);
                int result = this.fixturesService.Create(fixtures);

                return this.Ok($"{result} past fixtures added!");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postnextfixtures/{leagueId}/{number:int=10}")]
        public IActionResult GetNextFixture(int leagueId, int number = 10)
        {
            var client = new RestClient(string.Format(DataProvider.GetNexturesByLeagueIdUrl, leagueId, number));
            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);
            IRestResponse response = client.Execute(request);
            string content = response.Content;
            if (response.IsSuccessful)
            {
                var fixtures = JsonConvert.DeserializeObject<ImportApi>(content);
                int result = this.fixturesService.Create(fixtures);

                return this.Ok($"{result} next fixtures added!");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("posth2h/{team1id}/{team2id}")]
        public IActionResult GetHeadToHead(int team1id, int team2id)
        {
            var client = new RestClient(string.Format(DataProvider.GetHeadToHeadUrl, team1id, team2id));

            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);
            IRestResponse response = client.Execute(request);

            string content = response.Content;
            if (response.IsSuccessful)
            {
                var fixtures = JsonConvert.DeserializeObject<ImportApi>(content);
                int result = this.fixturesService.Create(fixtures);

                return this.Ok($"{result} head to head fixtures added!");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("poststandings/{leagueId}/")]
        public IActionResult GetStandings(int leagueId)
        {
            var client = new RestClient(string.Format(DataProvider.GetStandingsUrl, leagueId));
            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);
            IRestResponse response = client.Execute(request);

            string content = response.Content;
            if (response.IsSuccessful)
            {
                var fixtures = JsonConvert.DeserializeObject<ImportStandingsApi>(content);
                int result = this.standingsService.Create(fixtures);

                return this.Ok($"{result} head to head fixtures added!");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("poststatistics/{leagueId}/{teamId}")]
        public IActionResult GetStatisticsForTeam(int leagueId, int teamId)
        {
            var client = new RestClient(string.Format(DataProvider.GetTeamStatisticsUrl, leagueId, teamId));
            var request = new RestRequest(Method.GET);
            request = this.AddHeaders(request);
            IRestResponse response = client.Execute(request);

            string content = response.Content;

            if (response.IsSuccessful)
            {
                var statistics = JsonConvert.DeserializeObject<ImportStatisticsApi>(content);
                bool result = this.teamStatisticsService.AddStatistics(statistics, teamId, leagueId);

                if (result)
                {
                    return this.Ok($"Added statistics for team {teamId} for league {leagueId}");
                }
            }

            return this.BadRequest();
        }

        private RestRequest AddHeaders(RestRequest request)
        {
            request.AddHeader(DataProvider.ApiHost, DataProvider.ApiHostValue);
            request.AddHeader(DataProvider.ApiKey, DataProvider.ApiKeyValue);

            return request;
        }


    }
}