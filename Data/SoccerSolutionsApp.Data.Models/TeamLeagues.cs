namespace SoccerSolutionsApp.Data.Models
{
    using SoccerSolutionsApp.Data.Common.Models;
    using System;

    public class TeamLeagues : BaseDeletableModel<int>
    {
        
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public int LeagueId { get; set; }

        public virtual League League { get; set; }
    }
}
