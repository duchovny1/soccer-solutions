namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Web.ViewModels.Leagues;

    public class LeaguesController : Controller
    {
        private readonly ILeaguesService leaguesService;
        private readonly ICountriesService countriesService;
        private readonly IFixturesService fixturesService;

        public LeaguesController(
            ILeaguesService leaguesService,
            ICountriesService countriesService,
            IFixturesService fixturesService)
        {
            this.leaguesService = leaguesService;
            this.countriesService = countriesService;
            this.fixturesService = fixturesService;
        }

        public async Task<IActionResult> ById(string countryName, int leagueId, string leagueName)
        {
            var viewModel = new LeaguesByCountryNameViewModel();

            int countryId = await this.countriesService.GetCountryIdByName(countryName);
            viewModel.CurrentLeagueId = leagueId;
            viewModel.CountryName = countryName;
            viewModel.CurrentLeagueName = leagueName;
            viewModel.Leagues = await this.leaguesService.GetLeaguesByCountryId(countryId);
            viewModel.Fixtures = await this.fixturesService.GetFixturesForLeagueId(leagueId);

            return this.View(viewModel);
        }
    }
}