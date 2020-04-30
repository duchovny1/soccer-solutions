using SoccerSolutionsApp.Data.Models;
using SoccerSolutionsApp.Services.Mapping;
using SoccerSolutionsApp.Web.ViewModels.Leagues;
using System.Collections.Generic;

namespace SoccerSolutionsApp.Web.ViewModels.Competitions
{
    public class CountriesListingViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<LeaguesListingViewModel> Leagues { get; set; }
    }
}
