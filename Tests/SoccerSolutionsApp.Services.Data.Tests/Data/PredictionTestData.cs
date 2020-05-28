namespace SoccerSolutionsApp.Services.Data.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;

    public static class PredictionTestData
    {
        public static IEnumerable<Prediction> GetPredictions(int count, bool isPublic = true, bool sameUser = true)
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
                .Select(i => new Prediction
                {
                    Id = i,
                    Content = $"Prediction {i}",
                    GamePrediction = "1",
                    UserId = user.Id,
                })
                .ToList();

            return predictions;
        }

        public static PredictionsListingViewModel GetPredictionById(int id = 1)
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

            var model = new PredictionsListingViewModel()
            {
                Id = 1,
                Content = $"ASDADASDAFGHJLKJHGFDADSF",
                GamePrediction = "1",
                FixtureHomeTeamName = "Man City",
                FixtureAwayTeamName = "Liverpool",
                UserId = user.Id,
            };

            return model;
        }
    }
}
