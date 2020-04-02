﻿namespace SoccerSolutionsApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        public string Flag { get; set; }



    }
}
