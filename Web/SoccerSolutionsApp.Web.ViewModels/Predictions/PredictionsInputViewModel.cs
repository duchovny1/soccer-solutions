namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PredictionsInputViewModel
    {
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        [MaxLength(30)]
        public string Prediction { get; set; }

        // HEAD TO HEAD BUTTON
        public int? EventId { get; set; }
    }
}
