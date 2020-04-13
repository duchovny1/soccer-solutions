using Microsoft.EntityFrameworkCore;
using SoccerSolutionsApp.Data.Common.Repositories;
using SoccerSolutionsApp.Data.Models;
using System.Threading.Tasks;

namespace SoccerSolutionsApp.Services.Data.Seasons
{
    public class SeasonsService : ISeasonsService
    {
        private readonly IDeletableEntityRepository<Season> seasonsRepo;

        public SeasonsService(IDeletableEntityRepository<Season> seasonsRepo)
        {
            this.seasonsRepo = seasonsRepo;
        }

        public async Task CreateAsync(ImportSeasonsApi models)
        {
            foreach (var model in models.Api.Seasons)
            {
                string year = model;
                bool isExists = await this.seasonsRepo.All().AnyAsync(x => x.StartYear == year);

                if (!isExists)
                {
                    Season season = new Season()
                    {
                        StartYear = year,
                    };

                    await this.seasonsRepo.AddAsync(season);
                }
            }

            await this.seasonsRepo.SaveChangesAsync();
        }
    }
}
