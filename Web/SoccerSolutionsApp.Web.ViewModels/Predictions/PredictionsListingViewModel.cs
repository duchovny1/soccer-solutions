using SoccerSolutionsApp.Data.Models;
using SoccerSolutionsApp.Services.Mapping;
using System;

namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    public class PredictionsListingViewModel : IMapFrom<Prediction>
    {
        public string UserUsername { get; set; }

        public string Title => this.FixtureHomeTeamName + " - " + this.FixtureAwayTeamName;

        public string Content { get; set; }

        public string ShortContent => this.Content.Substring(0, 200) + "...";

        public string GamePrediction { get; set; }

        public DateTime FixtureKickOff { get; set; }

        public string FixtureHomeTeamName { get; set; }

        public string FixtureAwayTeamName { get; set; }

        public string FixtureHomeTeamLogo { get; set; }

        public string FixtureAwayTeamLogo { get; set; }
    }
}
