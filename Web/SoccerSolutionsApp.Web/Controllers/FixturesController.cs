namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Main;

    public class FixturesController : Controller
    {
        private const int LeaguesPerPage = 25;
        private readonly IFixturesService fixturesService;
        private readonly ILeaguesService leaguesService;

        public FixturesController(
            IFixturesService fixturesService,
            ILeaguesService leaguesService)
        {
            this.fixturesService = fixturesService;
            this.leaguesService = leaguesService;
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> ByDate(FixturesByDateInputModel model)
        {
            var viewModel = new FixturesByDateViewModel();
            viewModel.Leagues = this.leaguesService.GetAll<LeaguesListingViewModel>();

            foreach (var league in viewModel.Leagues)
            {
                league.NextFixturesForLeague = await this.fixturesService.GetFixtureForDate(league.Id, model.Date);
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int fixtureId)
        {
            var viewModel = await this.fixturesService.GetSingleFixtureById(fixtureId);

            if (viewModel == null)
            {
                return this.BadRequest();
            }

            viewModel.HomeTeamMatches = await this.fixturesService.GetPastFixturesForTeamByIdAsync(viewModel.HomeTeamId);
            viewModel.AwayTeamMatches = await this.fixturesService.GetPastFixturesForTeamByIdAsync(viewModel.AwayTeamId);

            return this.View(viewModel);
        }

    }
}