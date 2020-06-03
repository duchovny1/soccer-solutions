using Microsoft.AspNetCore.Mvc;
using SoccerSolutionsApp.Services.Data.Countries;
using SoccerSolutionsApp.Web.ViewModels.Teams;
using System.Linq;

namespace SoccerSolutionsApp.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
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