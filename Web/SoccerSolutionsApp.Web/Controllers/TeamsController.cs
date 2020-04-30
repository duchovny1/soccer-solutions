namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Data.TeamsServices;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class TeamsController : Controller
    {
        private const int FixturesPerPage = 20;
        private const int NexFixturePerPage = 4;

        private readonly ITeamsService teamsService;
        private readonly ICountriesService countriesService;
        private readonly IFixturesService fixturesService;
        private readonly ILeaguesService leaguesService;

        public TeamsController(
            ITeamsService teamsService,
            ICountriesService countriesService,
            IFixturesService fixturesService,
            ILeaguesService leaguesService)
        {
            this.teamsService = teamsService;
            this.countriesService = countriesService;

            this.fixturesService = fixturesService;
            this.leaguesService = leaguesService;
        }

        public IActionResult All()
        {
            var viewModel = this.teamsService.GetAll<TeamListingViewModel>();
            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int teamId)
        {
            var teamViewModel = await this.teamsService.GetTeamByIdAsync<TeamInfoViewModel>(teamId);

            int pages = await this.fixturesService.CountPastFixturesAsync(teamId);

            teamViewModel.PagesCount = (int)Math.Ceiling((double)pages / FixturesPerPage);
            teamViewModel.CurrentPage = teamViewModel.PagesCount;

            teamViewModel.PastFixtures = await this.fixturesService.GetPastFixturesForTeamByIdAsync(teamId, FixturesPerPage - NexFixturePerPage, (teamViewModel.PagesCount - 1) * FixturesPerPage);
            teamViewModel.NextFixtures = await this.fixturesService.GetNexTFixturesForTeamByIdAsync(teamId, NexFixturePerPage);

            return this.View(teamViewModel);
        }

        public async Task<IActionResult> ListingGamesById(int teamId, int page)
        {
            var teamViewModel = await this.teamsService.GetTeamByIdAsync<TeamInfoViewModel>(teamId);

            int pages = await this.fixturesService.CountPastFixturesAsync(teamId);

            teamViewModel.PagesCount = (int)Math.Ceiling((double)pages / FixturesPerPage);
            teamViewModel.CurrentPage = page;

            teamViewModel.PastFixtures = await this.fixturesService.GetPastFixturesForTeamByIdAsync(teamId, FixturesPerPage - NexFixturePerPage, (page - 1) * FixturesPerPage);
            teamViewModel.NextFixtures = await this.fixturesService.GetNexTFixturesForTeamByIdAsync(teamId, NexFixturePerPage);

            return this.View("ById", teamViewModel);
        }

        public async Task<IActionResult> AllGames(int teamId, int page = 1)
        {
            var viewModel = await this.teamsService.GetTeamByIdAsync<TeamInfoViewModel>(teamId);

            int pages = await this.fixturesService.CountPastFixturesAsync(teamId);

            viewModel.PagesCount = (int)Math.Ceiling((double)pages / 40);
            viewModel.CurrentPage = page;

            viewModel.PastFixtures = await this.fixturesService.GetAllPastFixturesByTeamId(teamId, 40 - 10, (page - 1) * FixturesPerPage);
            viewModel.NextFixtures = await this.fixturesService.GetAllNextFixtures(teamId);
            viewModel.Leagues = await this.leaguesService.GetLeaguesForTeam(teamId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> HomeGames(int teamId)
        {
            var viewModel = await this.teamsService.GetTeamByIdAsync<TeamInfoViewModel>(teamId);
            viewModel.PastFixtures = await this.fixturesService.GetAllPastWhereTeamIsHome(teamId);
            viewModel.Leagues = await this.leaguesService.GetLeaguesForTeam(teamId);

            return this.View("AllGames", viewModel);
        }

        public async Task<IActionResult> AwayGames(int teamId)
        {
            var viewModel = await this.teamsService.GetTeamByIdAsync<TeamInfoViewModel>(teamId);
            viewModel.PastFixtures = await this.fixturesService.GetAllPastWhereTeamIsAway(teamId);
            viewModel.Leagues = await this.leaguesService.GetLeaguesForTeam(teamId);

            return this.View("AllGames", viewModel);
        }

        public async Task<IActionResult> ByLeague(int teamId, int leagueId)
        {
            var viewModel = await this.teamsService.GetTeamByIdAsync<TeamInfoViewModel>(teamId);
            viewModel.PastFixtures = await this.fixturesService.GetAllPastForTeamAndLeague(teamId, leagueId);
            viewModel.Leagues = await this.leaguesService.GetLeaguesForTeam(teamId);

            return this.View("AllGames", viewModel);
        }
    }

}