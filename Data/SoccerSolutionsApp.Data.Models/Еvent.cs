namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;
    using SoccerSolutionsApp.Data.Models.Enums;

    public class Еvent : BaseDeletableModel<string>
    {
        public Еvent()
        {
            // because when we add game, game is not finished
            this.IsFinished = false;
        }

        public DateTime KickOff { get; set; }

        public bool IsFinished { get; set; }

        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        public int AwayTeamGoals { get; set; }

        public virtual FullTimeExit FullTimeExit { get; set; }

        [Required]
        public virtual League League { get; set; }
    }
}
