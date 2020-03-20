namespace SoccerSolutionsApp.Data.Models
{
    public class TeamLeagues
    {
        public int TeamId { get; set; }

        public Team Team { get; set; }

        public int LeagueId { get; set; }

        public League League { get; set; }

        public int TeamPointsInCurrentLeague { get; set; }
    }
}
