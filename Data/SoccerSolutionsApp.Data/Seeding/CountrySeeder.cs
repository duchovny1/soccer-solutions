namespace SoccerSolutionsApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Models;

    public class CountrySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Countries.AnyAsync())
            {
                return;
            }


            List<Country> countries = new List<Country>();

            countries.Add(new Country
            {
                Name = "England",
            });
            countries.Add(new Country
            {
                Name = "Germany",
            });
            countries.Add(new Country
            {
                Name = "Spain",
            });

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();
        }
    }
}
