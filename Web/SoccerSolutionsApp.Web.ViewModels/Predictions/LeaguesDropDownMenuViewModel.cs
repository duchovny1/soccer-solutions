namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class LeaguesDropDownMenuViewModel : IMapFrom<League>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
