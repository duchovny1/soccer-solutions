namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class CreatePredictionInputViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }

        public IEnumerable<CountriesDropDownViewModel> Countries { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int LeagueId { get; set; }

        public IEnumerable<LeaguesDropDownMenuViewModel> Leagues { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int FixtureId { get; set; }

        public IEnumerable<FixtureForLeagueDropDownModel> Fixtures { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        public string Prediction { get; set; }
      
        // HEAD TO HEAD BUTTON
       
    }
}
