using MyTested.AspNetCore.Mvc;
using SoccerSolutionsApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerSolutionsApp.Services.Data.Tests.Data
{
    public static class PredictionTestData
    {
        public static List<Prediction> GetPredictions(int count, bool isPublic = true, bool sameUser = true)
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
                    FixtureId = fixture.Id,
                    GamePrediction = "1",
                    UserId = user.Id,
                })
                .ToList();

            return predictions;
        }
    }
}
