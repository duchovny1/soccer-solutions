namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Statistics : IDeletableEntity
    {
        public int Id { get; set; }

        public Team Team { get; set; }

        [Required]
        public int TeamId { get; set; }


        public League League { get; set; }

        [Required]
        public int LeagueId { get; set; }

        public int MatchPlayedAsHomeTeam { get; set; }

        public int MatchPlayedAsAwayTeam { get; set; }

        public int MatchPlayedTotal { get; set; }

        public int MatchesWinsAsHome { get; set; }

        public int MatchesWinsAsAway { get; set; }

        public int MatchesWinsTotal { get; set; }

        public int MatchesDrawsAsHome { get; set; }

        public int MatchesDrawsAsAway { get; set; }

        public int MatchesDrawsTotal { get; set; }

        public int MatchesLosesAsHome { get; set; }

        public int MatchesLosesAsAway { get; set; }

        public int MatchesLosesTotal { get; set; }

        public int GoalsForAsHome { get; set; }

        public int GoalsForAsAway { get; set; }

        public int GoalsForTotal { get; set; }

        public int GoalsAgainstAsHome { get; set; }

        public int GoalsAgainstAsAway { get; set; }

        public int GoalsAgainstTotal { get; set; }

        public double GoalsForAvgAsHome { get; set; }

        public double GoalsForAvgAsAway { get; set; }

        public double GoalsForAvgTotal { get; set; }

        public double GoalsAgainstAvgAsHome { get; set; }

        public double GoalsAgainstAvgAsAway { get; set; }

        public double GoalsAgainstAvgTotal { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
