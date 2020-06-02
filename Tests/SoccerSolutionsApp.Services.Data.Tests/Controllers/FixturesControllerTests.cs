using MyTested.AspNetCore.Mvc;
using SoccerSolutionsApp.Data.Models;
using SoccerSolutionsApp.Services.Mapping;
using SoccerSolutionsApp.Web.Controllers;
using SoccerSolutionsApp.Web.ViewModels.Fixtures;
using System;
using System.Reflection;
using Xunit;

namespace SoccerSolutionsApp.Services.Data.Tests.Controllers
{
    public class FixturesControllerTests
    {
        public FixturesControllerTests()
        {
            this.InitializerMapper();
        }

        [Fact]
        public void ByDateMethodShouldReturnCorrectViewModelsWithCorrectPredictions()
        {

            var model = new FixturesByDateInputModel
            {
                Date = new DateTime(2020, 5, 31),
            };

            var result = MyController<FixturesController>
                .Instance()
                .WithData(x => x.
                        WithEntities(season => season.AddRange(
                            new Season { Id = 1, StartYear = "2019" }))
                        .WithEntities(leagues => leagues.AddRange(
                            new League { Id = 1, Name = "Premier League ", Type = "league", SeasonId = 1 },
                            new League { Id = 2, Name = "Championship ", Type = "league", SeasonId = 1 }))
                        .WithEntities(fixtures => fixtures.AddRange(
                            new Fixture { Id = 1, HomeTeamId = 1, AwayTeamId = 1, LeagueId = 2, KickOff = model.Date },
                            new Fixture { Id = 2, HomeTeamId = 1, AwayTeamId = 1, LeagueId = 2, KickOff = model.Date },
                            new Fixture { Id = 3, HomeTeamId = 2, AwayTeamId = 3, LeagueId = 3, KickOff = new DateTime(2020, 5, 30) })))
                .Calling(x => x.ByDate(model))
                .ShouldReturn()
                .View(result => result
                      .WithModelOfType<FixturesByDateViewModel>()
                      .Passing(x =>
                      {
                          Assert.NotEmpty(x.Leagues);
                      }));


        }

        private void InitializerMapper() => AutoMapperConfig.
                      RegisterMappings(Assembly.Load("SoccerSolutionsApp.Web.ViewModels"));
    }
}
