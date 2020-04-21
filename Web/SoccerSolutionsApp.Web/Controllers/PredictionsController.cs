namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class PredictionsController : Controller
    {
        private readonly IPredictionsService predictionsService;
        private readonly ICountriesService countriesService;
        private readonly ILeaguesService leaguesService;
        private readonly IFixturesService fixturesService;

        public PredictionsController(
            IPredictionsService predictionsService,
            ICountriesService countriesService,
            ILeaguesService leaguesService,
            IFixturesService fixturesService)
        {
            this.predictionsService = predictionsService;
            this.countriesService = countriesService;
            this.leaguesService = leaguesService;
            this.fixturesService = fixturesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var countries = this.countriesService.GetAll<CountriesDropDownViewModel>();

            var viewModel = new CreatePredictionInputViewModel()
            {
                Countries = countries,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePredictionInputViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.predictionsService.CreateAsync(model);
                return this.View();
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var predictions = await this.predictionsService.GetAll<PredictionsListingViewModel>();

            return this.View(predictions);
        }
    }
}