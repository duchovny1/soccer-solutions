namespace SoccerSolutionsApp.Web.ViewModels.Main
{
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class LeaguesListingViewModel : IMapFrom<League>
    {
        public int Id { get; set; }

        public string CountryName { get; set; }

        public string CountryFlag { get; set; }

        // league name
        public string Name { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<FixturesListingViewModel> NextFixturesForLeague { get; set; }
    }
}
