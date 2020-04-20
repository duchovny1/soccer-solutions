namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;
    using SoccerSolutionsApp.Data.Models.Enums;

    public class League : BaseDeletableModel<int>
    {
        public League()
        {
            this.TeamLeagues = new HashSet<TeamLeagues>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int SeasonId { get; set; }

        public virtual Season Season { get; set; }

        public DateTime? SeasonStart { get; set; }

        public DateTime? SeasonEnd { get; set; }

        public string LeagueShort { get; set; }

        public string Logo { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<TeamLeagues> TeamLeagues { get; set; }
    }
}
