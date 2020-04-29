namespace SoccerSolutionsApp.Web.ViewModels.H2H
{
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class H2HTeamsInfoViewModel : IMapFrom<Fixture>
    {
        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string HomeTeamLogo { get; set; }

        public string AwayTeamLogo { get; set; }
    }
}
