namespace SoccerSolutionsApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Prediction : IAuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        public int FixtureId { get; set; }

        public virtual Fixture Fixture { get; set; }

        [Required]
        [MaxLength(30)]
        public string GamePrediction { get; set; }

        public bool IsMatchFinished { get; set; }

        public bool? IsPredictionTrue { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
