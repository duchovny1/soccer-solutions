namespace SoccerSolutionsApp.Data.Models
{
    using SoccerSolutionsApp.Data.Common.Models;

    public class TeamLeagues : BaseDeletableModel<int>
    {
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public int LeagueId { get; set; }

        public virtual League League { get; set; }

        public int TeamPointsInCurrentLeague { get; set; }

        public int GamesPlayed { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsReceived { get; set; }
    }
}
