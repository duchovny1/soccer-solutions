namespace SoccerSolutionsApp.Data.Models
{
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Team : BaseDeletableModel<int>
    {
        public Team()
        {
            this.TeamPlayers = new HashSet<TeamPlayers>();
            this.TeamLeagues = new HashSet<TeamLeagues>();
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Logo { get; set; }

        public bool IsNational { get; set; }

        public int? Founded { get; set; }

        public string VenueName { get; set; }

        public int? VenueCapacity { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<TeamPlayers> TeamPlayers { get; set; }

        public ICollection<TeamLeagues> TeamLeagues { get; set; }
    }
}
