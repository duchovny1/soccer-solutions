namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class League
    {
        public League()
        {
            this.TeamLeagues = new HashSet<TeamLeagues>();
        }

        public int Id { get; set; }

        public int SeasonId { get; set; }

        public Season Season { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<TeamLeagues> TeamLeagues { get; set; }
    }
}
