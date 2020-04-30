namespace SoccerSolutionsApp.Web.ViewModels.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Leagues;

    public class CountriesAndLeaguesListingViewModel
    {
        public IEnumerable<CountriesListingViewModel> Countries { get; set; }

    }
}
