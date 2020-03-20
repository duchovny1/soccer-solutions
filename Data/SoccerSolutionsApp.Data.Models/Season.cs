namespace SoccerSolutionsApp.Data.Models
{
    using System;

    using SoccerSolutionsApp.Data.Common.Models;

    public class Season : BaseDeletableModel<int>
    {
        public DateTime StartYear { get; set; }

        public DateTime EndYear { get; set; }
    }
}
