using SoccerSolutionsApp.Data.Models;
using SoccerSolutionsApp.Services.Mapping;
using System;

namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    public class PredictionsListingViewModel : IMapFrom<Prediction>
    {
        public string UserIdentityName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string GamePrediction { get; set; }

        public DateTime EventKickOff { get; set; }

        public string EventHomeTeamName { get; set; }

        public string EventAwayTeamName { get; set; }

        public string EventHomeTeamLogo { get; set; }

        public string EventAwayTeamLogo { get; set; }
    }
}
