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
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Data.Seasons;
    using SoccerSolutionsApp.Services.Data.Standings;
    using SoccerSolutionsApp.Services.Data.Teams;
    using SoccerSolutionsApp.Services.Data.TeamsServices;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DataController : ControllerBase
    {
        private const int FixturesPerPage = 20;
        private const int NextFixturePerPage = 4;

        private readonly ApplicationDbContext db;
        private readonly ICountriesService countriesService;
        private readonly ISeasonsService seasonsService;
        private readonly ILeaguesService leaguesService;
        private readonly ITeamsService teamsService;
        private readonly IConfiguration configuration;
        private readonly IFixturesService fixturesService;
        private readonly IStandingsService standingsService;

        public DataController(
            ApplicationDbContext db,
            ICountriesService countriesService,
            ISeasonsService seasonsService,
            ILeaguesService leaguesService,
            ITeamsService teamsService,
            IConfiguration configuration,
            IFixturesService fixturesService,
            IStandingsService standingsService)
        {
            this.db = db;
            this.countriesService = countriesService;
            this.seasonsService = seasonsService;
            this.leaguesService = leaguesService;
            this.teamsService = teamsService;
            this.configuration = configuration;
            this.fixturesService = fixturesService;
            this.standingsService = standingsService;
        }

        public string ApiHostHeader => this.configuration.GetValue<string>("x-rapidapi:Host");

        public string ApiHostHeaderValue => this.configuration.GetValue<string>("x-rapidapi:HostValue");

        public string ApiKeyHeader => this.configuration.GetValue<string>("x-rapidapi:KeyHeader");

        public string ApiKeyHeaderValue => this.configuration.GetValue<string>("x-rapidapi:KeyHeaderValue");

        
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
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/countries");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");
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
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");

            IRestResponse response = client.Execute(request);
            string content = response.Content;

            if (response.IsSuccessful)
            {
                var seasons = JsonConvert.DeserializeObject<ImportSeasonsApi>(content);
                int result =  this.seasonsService.Create(seasons);

                return this.Ok($"{result} seasons added");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postleagues/{countryName}/{season}")]
        public IActionResult LeaguesByCountryAndYear(string countryName, int season)
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/leagues/country/{countryName}/{season}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");

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
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/teams/league/{leagueId}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");

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

                return this.Ok($"{result} past fixtures added!");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("postnextfixtures/{leagueId}/{number:int=10}")]
        public IActionResult GetNextFixture(int leagueId, int number = 10)
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/fixtures/league/{leagueId}/next/{number}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");
            IRestResponse response = client.Execute(request);
            string content = response.Content;
            if (response.IsSuccessful)
            {
                var fixtures = JsonConvert.DeserializeObject<ImportFixturesApi>(content);
                int result = this.fixturesService.Create(fixtures);

                return this.Ok($"{result} next fixtures added!");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("posth2h/{team1id}/{team2id}")]
        public IActionResult GetHeadToHead(int team1id, int team2id)
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/fixtures/h2h/{team1id}/{team2id}");

            var request = new RestRequest(Method.GET);
            request.AddHeader(this.ApiHostHeader, this.ApiHostHeaderValue);
            request.AddHeader(this.ApiKeyHeader, this.ApiKeyHeaderValue);
            IRestResponse response = client.Execute(request);

            string content = response.Content;
            if (response.IsSuccessful)
            {
                var fixtures = JsonConvert.DeserializeObject<ImportFixturesApi>(content);
                int result = this.fixturesService.Create(fixtures);

                return this.Ok($"{result} head to head fixtures added!");
            }

            return this.BadRequest();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet("getstandings/{leagueId}/")]
        public IActionResult GetStandings(int leagueId)
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/leagueTable/{leagueId}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");
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
      
    }
}