namespace SoccerSolutionsApp.Web.ViewModels.Fixtures
{
    using System;
    using System.Collections.Generic;

    using SoccerSolutionsApp.Web.ViewModels.Main;

    public class FixturesByDateViewModel
    {
        public IEnumerable<LeaguesListingViewModel> Leagues { get; set; }

        public DateTime CurrentDate { get; set; }

        public DateTime MinDate { get; set; }

        public DateTime MaxDate { get; set; }

    }
}
