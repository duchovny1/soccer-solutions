namespace SoccerSolutionsApp.Data.Models
{
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Common.Models;

    public class League : BaseDeletableModel<int>
    {
        public League()
        {
            this.TeamLeagues = new HashSet<TeamLeagues>();
        }

        public string Name { get; set; }

        public int SeasonId { get; set; }

        public virtual Season Season { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<TeamLeagues> TeamLeagues { get; set; }
       
    }
}
