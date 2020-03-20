namespace SoccerSolutionsApp.Data.Models
{
    using SoccerSolutionsApp.Data.Common.Models;

    public class TeamPlayers : BaseDeletableModel<int>
    {
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
