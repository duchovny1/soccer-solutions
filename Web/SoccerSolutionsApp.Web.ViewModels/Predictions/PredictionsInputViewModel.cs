namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PredictionsInputViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        [MaxLength(30)]
        public string Prediction { get; set; }

        public string HomeTeamLogo { get; set; }

        public string AwayTeamLogo { get; set; }

        // HEAD TO HEAD BUTTON
        public int? EventId { get; set; }
    }
}
