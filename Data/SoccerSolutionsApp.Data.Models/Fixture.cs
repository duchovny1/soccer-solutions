namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;
    using SoccerSolutionsApp.Data.Models.Enums;

    public class Fixture : BaseDeletableModel<int>
    {
        public DateTime KickOff { get; set; }

        public TimeSpan EventTimeSpan { get; set; }

        public string Venue { get; set; }

        public string Referee { get; set; }

        public string Round { get; set; }

        public Status Status { get; set; }

        public string StatusShort { get; set; }

        public int Elapsed { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        [Range(0, 30)]
        public int? GoalsHomeTeam { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }


        [Range(0, 30)]
        public int? GoalsAwayTeam { get; set; }

        public virtual FullTimeExit FullTimeExit { get; set; }

        [Required]
        public int LeagueId { get; set; }

        public virtual League League { get; set; }

        public string Halftime { get; set; }

        public string Fulltime { get; set; }

        public string Extratime { get; set; }

        public string Penalty { get; set; }
    }
}
