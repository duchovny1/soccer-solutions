﻿namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using System;
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class PredictionsListingViewModel : IMapFrom<Prediction>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

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
