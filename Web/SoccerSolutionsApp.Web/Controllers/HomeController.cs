namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Web.ViewModels;
    using SoccerSolutionsApp.Web.ViewModels.Main;

    public class HomeController : BaseController
    {
        private readonly ILeaguesService leaguesService;
        private readonly IFixturesService fixturesService;

        public HomeController(
            ILeaguesService leaguesService,
            IFixturesService fixturesService)
        {
            this.leaguesService = leaguesService;
            this.fixturesService = fixturesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = this.leaguesService.GetAll<LeaguesListingViewModel>();

            foreach (var model in viewModel)
            {
                model.NextFixturesForLeague = await this.fixturesService.GetNextFixturesByIdAsync(model.Id);
            }
            
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
