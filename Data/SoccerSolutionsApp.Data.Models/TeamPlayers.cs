namespace SoccerSolutionsApp.Data.Models
{
    public class TeamPlayers
    {
        public int TeamId { get; set; }

        public Team Team { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
