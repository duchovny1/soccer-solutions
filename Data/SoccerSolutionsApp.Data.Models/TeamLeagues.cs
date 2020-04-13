namespace SoccerSolutionsApp.Data.Models
{
    using System;

    using SoccerSolutionsApp.Data.Common.Models;

    public class TeamLeagues : IAuditInfo, IDeletableEntity
    {
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public int LeagueId { get; set; }

        public virtual League League { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
