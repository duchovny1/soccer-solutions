namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Authenticators;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Data;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Data.Seasons;

    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly ICountriesService countriesService;
        private readonly ISeasonsService seasonsService;
        private readonly ILeaguesService leaguesService;

        public DataController(
            ApplicationDbContext db,
            ICountriesService countriesService,
            ISeasonsService seasonsService,
            ILeaguesService leaguesService)
        {
            this.db = db;
            this.countriesService = countriesService;
            this.seasonsService = seasonsService;
            this.leaguesService = leaguesService;
        }

        [HttpGet("getleagues")]
        public ActionResult<SelectList> GetLeagues(int countryId)
        {
            // its used by cascading drop down menu in the view
            var leagues = new SelectList(this.db.Leagues.Where(c => c.CountryId == countryId).ToList(), "Id", "Name");

            return leagues;
        }

        // getting data from external Api

        [HttpGet("postcountries")]
        public async Task<IActionResult> Countries()
        {

            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/countries");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");
            IRestResponse response = await client.ExecuteAsync(request);
            string content = response.Content;
            if (response.IsSuccessful)
            {
                var countries = JsonConvert.DeserializeObject<ImportApi>(content);
                await this.countriesService.CreateAsync(countries);

                return this.Ok();
            }

            return this.BadRequest();
        }

        [HttpGet("postseasons")]
        public async Task<IActionResult> Seasons()
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/seasons");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");

            IRestResponse response = await client.ExecuteAsync(request);
            string content = response.Content;

            if (response.IsSuccessful)
            {
                var seasons = JsonConvert.DeserializeObject<ImportSeasonsApi>(content);
                await this.seasonsService.CreateAsync(seasons);

                return this.Ok();
            }

            return this.BadRequest();
        }

        [HttpGet("{countryName}/{season}")]
        public async Task<IActionResult> LeaguesByCountryAndYear(string countryName, int season)
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/leagues/country/{countryName}/{season}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da");

            IRestResponse response = await client.ExecuteAsync(request);
            string content = response.Content;

            if (response.IsSuccessful)
            {
                var leagues = JsonConvert.DeserializeObject<ImportLeaguesApi>(content);
                await this.leaguesService.CreateAsync(leagues);

                return this.Ok();
            }

            return this.BadRequest();
        }
    }
}