namespace SoccerSolutionsApp.Data.Models
{
    using System.Collections.Generic;

    public class Player
    {
        public Player()
        {
            this.TeamsPlayer = new HashSet<TeamPlayers>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TeamPlayers> TeamsPlayer { get; set; }
    }
}
