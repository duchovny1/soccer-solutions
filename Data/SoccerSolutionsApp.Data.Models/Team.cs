namespace SoccerSolutionsApp.Data.Models
{
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.TeamPlayers = new HashSet<TeamPlayers>();
            this.TeamLeagues = new HashSet<TeamLeagues>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TeamPlayers> TeamPlayers { get; set; }

        public ICollection<TeamLeagues> TeamLeagues { get; set; }
    }
}
