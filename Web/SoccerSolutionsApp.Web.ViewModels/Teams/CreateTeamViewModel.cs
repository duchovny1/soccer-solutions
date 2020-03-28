namespace SoccerSolutionsApp.Web.ViewModels.Teams
{
    using SoccerSolutionsApp.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateTeamViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }

        public IEnumerable<CountriesDropDownViewModel> Countries { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int LeagueId { get; set; }

        public IEnumerable<League> Leagues { get; set; }

        public string? Logo { get; set; }
    }
}
