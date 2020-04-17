namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;

    public class FixturesController : Controller
    {
        private readonly IFixturesService fixturesService;

        public FixturesController(IFixturesService fixturesService)
        {
            this.fixturesService = fixturesService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FixturesByDate(FixturesByDateInputModel model)
        {
            var fixtures = this.fixturesService.GetFixturesByDate(model);

            return this.RedirectToAction("ByDate", "Fixtures", fixtures);
        }
    }
}