namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;
    using SoccerSolutionsApp.Data.Models.Enums;

    public class Fixture : BaseDeletableModel<int>
    {
        public Fixture()
        {
            // because when we add game, game is not finished
            this.IsFinished = false;
        }

        public DateTime KickOff { get; set; }

        public TimeSpan EventTimeSpan { get; set; }

        public string Venue { get; set; }

        public string Referee { get; set; }

        public bool IsFinished { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        [Required]
        [Range(0, 30)]
        public int GoalsHomeTeam { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        [Required]
        [Range(0, 30)]
        public int GoalsAwayTeam { get; set; }

        public virtual FullTimeExit FullTimeExit { get; set; }

        [Required]
        public virtual League League { get; set; }
    }
}
