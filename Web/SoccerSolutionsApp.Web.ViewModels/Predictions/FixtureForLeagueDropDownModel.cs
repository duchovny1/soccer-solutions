namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using System.Text;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class FixtureForLeagueDropDownModel : IMapFrom<Fixture>
    {
        public int Id { get; set; }

        public string Name => new StringBuilder().AppendLine($"{this.HomeTeamName} - {this.AwayTeamName}").ToString();

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }
    }
}
