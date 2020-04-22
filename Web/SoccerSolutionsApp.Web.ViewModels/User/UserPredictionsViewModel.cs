namespace SoccerSolutionsApp.Web.ViewModels.User
{
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class UserPredictionsViewModel : IMapFrom<Prediction>
    {
        public string Title => this.FixtureHomeTeamName + " - " + this.FixtureAwayTeamName;

        public string FixtureHomeTeamName { get; set; }

        public string FixtureAwayTeamName { get; set; }

        public string GamePrediction { get; set; }

        public string IsMatchFinished { get; set; }

        public bool? IsPredictionTrue { get; set; }
    }
}
