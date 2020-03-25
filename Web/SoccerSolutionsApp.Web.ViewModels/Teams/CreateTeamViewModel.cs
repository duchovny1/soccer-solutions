namespace SoccerSolutionsApp.Web.ViewModels.Teams
{
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
    }
}
