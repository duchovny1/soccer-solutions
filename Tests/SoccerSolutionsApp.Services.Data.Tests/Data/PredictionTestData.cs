namespace SoccerSolutionsApp.Services.Data.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;

    public static class PredictionTestData
    {
        public static IEnumerable<PredictionsListingViewModel> GetPredictions(int count, bool isPublic = true, bool sameUser = true)
        {

            ApplicationUser user = new ApplicationUser
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username,
            };

            var fixture = new Fixture()
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                GoalsAwayTeam = 0,
                GoalsHomeTeam = 2,
                LeagueId = 2,
            };

            var predictions = Enumerable
                .Range(1, count)
                .Select(i => new PredictionsListingViewModel
                {
                    Id = i,
                    Content = $"Prediction {i}",
                    GamePrediction = "1",
                    FixtureHomeTeamName = "Man City",
                    FixtureAwayTeamName = "Liverpool",
                    UserId = user.Id,
                })
                .ToList();

            return predictions;
        }
    }
}
