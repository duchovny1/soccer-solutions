namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.H2H;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.H2H;
    using SoccerSolutionsApp.Web.ViewModels.Main;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class FixturesController : Controller
    {
        private const int LeaguesPerPage = 25;
        private readonly IFixturesService fixturesService;
        private readonly ILeaguesService leaguesService;
        private readonly IH2HService h2HService;

        public FixturesController(
            IFixturesService fixturesService,
            ILeaguesService leaguesService,
            IH2HService h2HService)
        {
            this.fixturesService = fixturesService;
            this.leaguesService = leaguesService;
            this.h2HService = h2HService;
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

            //hardcode values -> im taking 8 past matches for each team plus 2 next matches for each team!

            viewModel.HomeTeamPastMatches = await this.fixturesService.GetPastFixturesForTeamByIdAsync(viewModel.HomeTeamId, 8);
            viewModel.HomeTeamNextMatches = await this.fixturesService.GetNexTFixturesForTeamByIdAsync(viewModel.HomeTeamId, 2);
            viewModel.AwayTeamPastMatches = await this.fixturesService.GetPastFixturesForTeamByIdAsync(viewModel.AwayTeamId, 8);
            viewModel.HomeTeamNextMatches = await this.fixturesService.GetNexTFixturesForTeamByIdAsync(viewModel.HomeTeamId, 2);

            return this.View(viewModel);
        }


        public async Task<IActionResult> Head2Head(int fixtureId, int hometeamId, int awayTeamId)
        {
            H2HViewModel viewModel = await this.h2HService.PrepareViewModel(hometeamId, awayTeamId);
            viewModel.TeamsDetails = await this.fixturesService.GetH2HTeamsInfo(fixtureId);
            viewModel.Fixtures = await this.fixturesService.GetHead2Head(hometeamId, awayTeamId);

            return this.View(viewModel);
        }
    }
}