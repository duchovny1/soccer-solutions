namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Countries;
    using SoccerSolutionsApp.Services.TeamsServices;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;
        private readonly ICountriesService countriesService;

        public TeamsController(
            ITeamsService teamsService,
            ICountriesService countriesService)
        {
            this.teamsService = teamsService;
            this.countriesService = countriesService;
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
    }
}