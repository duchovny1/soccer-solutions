namespace SoccerSolutionsApp.Web.ViewModels.Leagues
{
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class LeaguesListingViewModel : IMapFrom<League>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
