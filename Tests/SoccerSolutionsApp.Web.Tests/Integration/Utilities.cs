namespace SoccerSolutionsApp.Web.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Net.Http.Headers;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;

    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Seasons.AddRange(GetSeasons());
            db.Countries.AddRange(GetCountries());
            db.Leagues.AddRange(GetLeagues());
            db.Teams.AddRange(GetTeams());
            db.Users.AddRange(GetUsers());
            db.Fixtures.AddRange(GetFixtures());
            db.Predictions.AddRange(GetPredictions());

            db.SaveChanges();
        }

        private static List<Country> GetCountries()
        {
            return new List<Country>()
            {
                new Country() { Id = 1, Name = "England", Code = "EN" },
            };
        }

        private static List<Season> GetSeasons()
        {
            return new List<Season>()
            {
                new Season() { Id = 1, StartYear = "2019"},
            };

        }

        private static List<League> GetLeagues()
        {
            return new List<League>()
            {
                new League() { Id = 1, Name = "Premier League", Type = "League", SeasonId = 1, CountryId = 1},
                new League() { Id = 2, Name = "Championship", Type = "League", SeasonId = 1, CountryId = 1 },
            };
        }

        private static List<Team> GetTeams()
        {
            return new List<Team>()
            {
                 new Team()
            {
                Id = 1,
                Name = "Liverpool",
                CountryId = 1,
            },
                 new Team()
             {
                 Id = 2,
                 Name = "Arsenal",
                 CountryId = 1,
             },
            };
        }

        private static List<Fixture> GetFixtures() => new List<Fixture>()
        {
            // this fixture is not started.
                new Fixture()
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 1,
                LeagueId = 1,
                KickOff = DateTime.Today.AddDays(1),
                Status = Status.NotStarted,
            },

        };

        private static List<ApplicationUser> GetUsers() => new List<ApplicationUser>()
        {
            new ApplicationUser()
            {
                Id = "1",
                UserName = "testuser@abv.bg",
            },
            new ApplicationUser()
            {
                Id = "2",
                UserName = "anothertestuser@abv.bg",
            },
        };

        private static List<Prediction> GetPredictions() => new List<Prediction>()
        {
            new Prediction()
            {
                FixtureId = 1,
                Content = "Game should be 1",
                GamePrediction = "1",
                UserId = "1",
            },
            new Prediction()
            {
                FixtureId = 1,
                Content = "Game should be 2",
                GamePrediction = "X",
                UserId = "2",
            },
        };

    }
}

