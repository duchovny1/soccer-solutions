namespace SoccerSolutionsApp.Web.ViewModels.Teams
{
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class TeamListingViewModel : IMapFrom<Team>
    {
        public string Name { get; set; }
    }
}
