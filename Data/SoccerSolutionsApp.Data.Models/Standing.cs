namespace SoccerSolutionsApp.Data.Models
{
    using SoccerSolutionsApp.Data.Common.Models;

    public class Standing : BaseDeletableModel<int>
    {
        public int Rank { get; set; }

        public int LeagueId { get; set; }

        public League League { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public string Group { get; set; }

        public string Forme { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    
        public int GoalsDiff { get; set; }

        public int Points { get; set; }

        public int MatchPlayed { get; set; }

        public int Win { get; set; }

        public int Draw { get; set; }

        public int Lose { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }
    }
}
