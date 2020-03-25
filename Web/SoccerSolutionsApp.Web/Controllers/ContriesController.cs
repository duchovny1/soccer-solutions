using Microsoft.AspNetCore.Mvc;
using SoccerSolutionsApp.Services.Countries;
using SoccerSolutionsApp.Web.ViewModels.Teams;
using System.Linq;

namespace SoccerSolutionsApp.Web.Controllers
{
    public class ContriesController : Controller
    {
        private readonly ICountriesService countriesService;

        public ContriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Create()
        {
            var countries = this.countriesService.GetAll<CountriesDropDownViewModel>().OrderBy(x => x.Name);

            return this.View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}