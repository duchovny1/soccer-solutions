namespace SoccerSolutionsApp.Web.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;

    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Seasons.AddRange(GetSeasons());
            db.Countries.AddRange(GetCountries());
            db.Leagues.AddRange(GetLeagues());

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
    }
}
