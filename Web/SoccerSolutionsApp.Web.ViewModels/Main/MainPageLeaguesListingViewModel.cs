namespace SoccerSolutionsApp.Web.ViewModels.Main
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MainPageLeaguesListingViewModel
    {
        public IEnumerable<LeaguesListingViewModel> Leagues { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
