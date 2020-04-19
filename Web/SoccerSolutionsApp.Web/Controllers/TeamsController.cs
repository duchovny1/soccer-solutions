namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Countries;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.TeamsServices;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;
        private readonly ICountriesService countriesService;
        private readonly ApplicationDbContext dbContext;
        private readonly IFixturesService fixturesService;

        public TeamsController(
            ITeamsService teamsService,
            ICountriesService countriesService,
            ApplicationDbContext dbContext,
            IFixturesService fixturesService)
        {
            this.teamsService = teamsService;
            this.countriesService = countriesService;
            this.dbContext = dbContext;
            this.fixturesService = fixturesService;
        }

        public IActionResult All()
        {
            var viewModel = this.teamsService.GetAll<TeamListingViewModel>();
            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var countries = this.countriesService.GetAll<CountriesDropDownViewModel>();
            var viewModel = new CreateTeamViewModel()
            {
                Countries = countries,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int teamId)
        {
            var teamViewModel = await this.teamsService.GetTeamByIdAsync<TeamInfoViewModel>(teamId);

            teamViewModel.PastFixtures = await this.fixturesService.GetPastFixturesForTeamById(teamId, 8);
            teamViewModel.NextFixtures = await this.fixturesService.GetNexTFixturesForTeamByIdAsync(teamId, 8);

            return this.View(teamViewModel);
        }
    }

}