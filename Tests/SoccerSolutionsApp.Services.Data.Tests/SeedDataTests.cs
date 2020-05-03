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
                StartYear = "2019",
            };
        }

        public static List<Country> GetCountriesData()
        {
            return new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    Name = "England",
                },
            };
        }

        public static List<League> GetLeaguesData()
        {
            return new List<League>()
            {
                new League()
                {
                    Id = 1,
                    Name = "Premier League",
                    Type = "League",
                    SeasonId = 1,
                },
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
            },
                new Team()
             {
                 Id = 2,
                 Name = "Arsenal",
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
                 KickOff = DateTime.Today.AddDays(-100),
                 Status = Status.MatchFinished,
                 FullTimeExit = FullTimeExit.Draw,
             },
            };
    }
}
