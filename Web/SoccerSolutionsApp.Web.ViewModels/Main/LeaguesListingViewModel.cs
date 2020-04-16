using SoccerSolutionsApp.Data.Models;
using SoccerSolutionsApp.Services.Mapping;

namespace SoccerSolutionsApp.Web.ViewModels.Main
{
    public class LeaguesListingViewModel : IMapFrom<League>
    {
        public string CountryName { get; set; }

        public string CountryFlag { get; set; }

        // league name
        public string Name { get; set; }
    }
}
