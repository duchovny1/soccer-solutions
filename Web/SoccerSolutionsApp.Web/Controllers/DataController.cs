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

    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly ICountriesService countriesService;

        public DataController(
            ApplicationDbContext db,
            ICountriesService countriesService)
        {
            this.db = db;
            this.countriesService = countriesService;
        }

        [HttpGet("getleagues")]
        public ActionResult<SelectList> GetLeagues(int countryId)
        {
            var leagues = new SelectList(this.db.Leagues.Where(c => c.CountryId == countryId).ToList(), "Id", "Name");

            return leagues;
        }

        // getting teams from external Api
        // getting teams by league Id
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
    }
}