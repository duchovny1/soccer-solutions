namespace SoccerSolutionsApp.Data.Models
{
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Player : BaseDeletableModel<int>
    {
        public Player()
        {
            this.TeamsPlayer = new HashSet<TeamPlayers>();
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public virtual ICollection<TeamPlayers> TeamsPlayer { get; set; }
    }
}
