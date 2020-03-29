﻿namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Countries;
    using SoccerSolutionsApp.Services.TeamsServices;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;
        private readonly ICountriesService countriesService;
        private readonly ApplicationDbContext dbContext;

        public TeamsController(
            ITeamsService teamsService,
            ICountriesService countriesService,
            ApplicationDbContext dbContext)
        {
            this.teamsService = teamsService;
            this.countriesService = countriesService;
            this.dbContext = dbContext;
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