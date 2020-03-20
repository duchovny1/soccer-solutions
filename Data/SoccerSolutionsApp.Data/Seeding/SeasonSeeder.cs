namespace SoccerSolutionsApp.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Models;

    public class SeasonSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
           if (await dbContext.Seasons.AnyAsync())
            {
                return;
            }

           await dbContext.Seasons.AddAsync(new Season
            {
                StartYear = new DateTime(2019, 8, 10),
                EndYear = new DateTime(2020, 5, 17),
            });

           await dbContext.SaveChangesAsync();
        }
    }
}
