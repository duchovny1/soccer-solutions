namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Web.ViewModels;
    using SoccerSolutionsApp.Web.ViewModels.Main;

    public class HomeController : BaseController
    {
        private const int LeaguesPerPage = 25;
        private readonly ILeaguesService leaguesService;
        private readonly IFixturesService fixturesService;
        private readonly IDeletableEntityRepository<ApplicationUser> repo;
        private readonly UserManager<ApplicationUser> manager;

        public HomeController(
            ILeaguesService leaguesService,
            IFixturesService fixturesService,
            IDeletableEntityRepository<ApplicationUser> _repo,
            UserManager<ApplicationUser> manager)
        {
            this.leaguesService = leaguesService;
            this.fixturesService = fixturesService;
            repo = _repo;
            this.manager = manager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var viewModel = new MainPageLeaguesListingViewModel();
            viewModel.Leagues = this.leaguesService.GetAll<LeaguesListingViewModel>(LeaguesPerPage, (page - 1) * LeaguesPerPage);

            foreach (var model in viewModel.Leagues)
            {
                model.NextFixturesForLeague = await this.fixturesService.GetNextFixturesByLeagueIdAsync(model.Id);
            }

            int pages = await this.leaguesService.CountAsync();

            viewModel.PagesCount = (int)Math.Ceiling((double)pages / LeaguesPerPage);
            viewModel.CurrentPage = page;
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
