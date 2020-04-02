namespace SoccerSolutionsApp.Data.Models
{
    using System;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Season : BaseDeletableModel<int>
    {
        public string StartYear { get; set; }
    }
}
