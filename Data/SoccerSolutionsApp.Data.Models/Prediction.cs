namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Prediction : BaseModel<int>
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        [MaxLength(30)]
        public string GamePrediction { get; set; }

        public string HomeTeamLogo { get; set; }

        public string AwayTeamLogo { get; set; }

        public int? EventId { get; set; }

        public virtual Fixture Еvent { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
