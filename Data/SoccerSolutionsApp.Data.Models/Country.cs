namespace SoccerSolutionsApp.Data.Models
{
    using SoccerSolutionsApp.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
