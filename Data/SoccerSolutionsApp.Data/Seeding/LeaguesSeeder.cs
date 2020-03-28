namespace SoccerSolutionsApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Models;

    public class LeaguesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Leagues.AnyAsync())
            {
                return;
            }

            List<League> leagues = new List<League>();

            leagues.Add(new League
            {
                Name = "Premier League",
                CountryId = 1,
                SeasonId = 1,
            });
            leagues.Add(new League
            {
                Name = "Championship",
                CountryId = 1,
                SeasonId = 1,
            });
            leagues.Add(new League
            {
                Name = "FA Cup",
                CountryId = 1,
                SeasonId = 1,
            });
            leagues.Add(new League
            {
                Name = "Bundesliga",
                CountryId = 2,
                SeasonId = 1,
            });

            await dbContext.AddRangeAsync(leagues);
            await dbContext.SaveChangesAsync();
        }
    }
}

