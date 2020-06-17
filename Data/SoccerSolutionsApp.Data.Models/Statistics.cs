namespace SoccerSolutionsApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Statistics
    {
        public int Id { get; set; }

        public Team Team { get; set; }

        [Required]
        public int TeamId { get; set; }

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

        public int GoasAgainstAsAway { get; set; }

        public int GoalsAgainstTotal { get; set; }
    }
}
