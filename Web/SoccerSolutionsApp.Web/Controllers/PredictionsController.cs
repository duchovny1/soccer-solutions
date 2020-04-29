namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Services.Data.Users;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class PredictionsController : Controller
    {
        private readonly IPredictionsService predictionsService;
        private readonly ICountriesService countriesService;
        private readonly ILeaguesService leaguesService;
        private readonly IFixturesService fixturesService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public PredictionsController(
            UserManager<ApplicationUser> userManager,
            IPredictionsService predictionsService,
            ICountriesService countriesService,
            ILeaguesService leaguesService,
            IFixturesService fixturesService,
            IUsersService usersService
            )
        {
            this.predictionsService = predictionsService;
            this.countriesService = countriesService;
            this.leaguesService = leaguesService;
            this.fixturesService = fixturesService;
            this.usersService = usersService;
            this.userManager = userManager;
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
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.predictionsService.CreateAsync(model, user.Id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new PredictionsListingAndFollowingsViewModel()
            {
                Predictions = await this.predictionsService.GetAll<PredictionsListingViewModel>(),
            };

            if (userId == null)
            {
                model.CurrentUserFollowings = new HashSet<string>();
            }
            else
            {
                model.CurrentUserFollowings = await this.usersService.GetCurrentUserFollowingsAsync(userId);
            }

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> GetFollowingsPredictions()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new PredictionsListingAndFollowingsViewModel()
            {
                Predictions = await this.predictionsService.GetFollowingsPredictions(userId),
            };

            if (userId == null)
            {
                model.CurrentUserFollowings = new HashSet<string>();
            }
            else
            {
                model.CurrentUserFollowings = await this.usersService.GetCurrentUserFollowingsAsync(userId);
            }

            return this.View("All", model);
        }
    }
}