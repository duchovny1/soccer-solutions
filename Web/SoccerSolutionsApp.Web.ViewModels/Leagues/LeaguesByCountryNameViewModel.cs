namespace SoccerSolutionsApp.Web.ViewModels.Leagues
{
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using System.Collections.Generic;

    public class LeaguesByCountryNameViewModel
    {
        public int CurrentLeagueId { get; set; }

        public string CurrentLeagueName { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<LeaguesListingViewModel> Leagues { get; set; }

        public IEnumerable<PastFixturesViewModel> Fixtures { get; set; }
    }
}
