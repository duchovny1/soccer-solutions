namespace SoccerSolutionsApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;

    public static class SeedDataTests
    {
        public static Season GetSeasonData()
        {
            return new Season()
            {
                Id = 1,
                StartYear = "2019",            };
        }

        public static Country GetCountriesData()
        {
            return new Country()
            {
                Id = 1,
                Name = "England",
                Code = "EN",
            };
        }

        public static League GetLeaguesData()
        {
            return new League()
            {
                Id = 1,
                Name = "Premier League",
                Type = "League",
                SeasonId = 1,
                CountryId = 1,
            };
        }

        public static List<Team> GetTeamsData()
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



        public static List<Fixture> GetFixturesData() => new List<Fixture>()
            {
                new Fixture()
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                GoalsHomeTeam = 0,
                GoalsAwayTeam = 2,
                LeagueId = 1,
                KickOff = DateTime.Today.AddDays(-60),
                Status = Status.MatchFinished,
                FullTimeExit = FullTimeExit.AwayWin,
            },
                new Fixture()
             {
                 Id = 2,
                 HomeTeamId = 2,
                 AwayTeamId = 1,
                 GoalsHomeTeam = 0,
                 GoalsAwayTeam = 3,
                 LeagueId = 1,
                 KickOff = DateTime.Today.AddDays(-30),
                 Status = Status.MatchFinished,
                 FullTimeExit = FullTimeExit.AwayWin,
             },

                new Fixture()
            {
                Id = 3,
                HomeTeamId = 1,
                AwayTeamId = 2,
                GoalsHomeTeam = 3,
                GoalsAwayTeam = 0,
                LeagueId = 1,
                KickOff = DateTime.Today.AddDays(-90),
                Status = Status.MatchFinished,
                FullTimeExit = FullTimeExit.HomeWin,
            },

                new Fixture()
             {
                 Id = 4,
                 HomeTeamId = 2,
                 AwayTeamId = 1,
                 GoalsHomeTeam = 1,
                 GoalsAwayTeam = 1,
                 LeagueId = 1,
                 KickOff = DateTime.Today.AddDays(-100),
                 Status = Status.MatchFinished,
                 FullTimeExit = FullTimeExit.Draw,
             },
                new Fixture()
             {
                 Id = 5,
                 HomeTeamId = 2,
                 AwayTeamId = 1,
                 GoalsHomeTeam = 1,
                 GoalsAwayTeam = 1,
                 LeagueId = 1,
                 KickOff = DateTime.Today.AddDays(1),
                 Status = Status.NotStarted,
             },
            };

        public static List<ApplicationUser> GetUsers() => new List<ApplicationUser>()
        {
            new ApplicationUser()
            {
                UserName = "testuser@abv.bg",
            },
            new ApplicationUser()
            {
                UserName = "anothertestuser@abv.bg",
            },
        };

        public static List<Prediction> GetPredictionsDataForUserOne(string userId) => new List<Prediction>()
        {
            new Prediction()
            {
                FixtureId = 5,
                Content = "asdajidasda",
                GamePrediction = "1",
                UserId = userId,
            },

            new Prediction()
            {
                FixtureId = 5,
                Content = "asdajidasda",
                GamePrediction = "X",
                UserId = userId,
            },
        };


        public static List<Prediction> GetPredictionsDataForUserTwo(string userId) => new List<Prediction>()
        {
            new Prediction()
            {
                FixtureId = 5,
                Content = "asdajidasda",
                GamePrediction = "1",
                UserId = userId,
            },
            new Prediction()
            {
                FixtureId = 5,
                Content = "asdajidasda",
                GamePrediction = "X",
                UserId = userId,
            },
            new Prediction()
            {
                FixtureId = 5,
                Content = "asdajidasda",
                GamePrediction = "X",
                UserId = userId,
            },
        };

    }
}
