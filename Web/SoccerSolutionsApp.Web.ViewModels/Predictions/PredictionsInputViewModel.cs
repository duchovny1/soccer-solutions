namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using System.ComponentModel.DataAnnotations;

    public class PredictionsInputViewModel
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        public string KickOff { get; set; }

        // HEAD TO HEAD BUTTON
        public int? EventId { get; set; }

        [Required]
        public string UserId { get; set; }

        public string EventHomeTeamLogo { get; set; }

        public string EventAwayTeamLogo { get; set; }
    }
}
